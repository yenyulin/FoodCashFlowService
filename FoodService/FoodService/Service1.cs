using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Net;
using System.Web;
using FoodOrg;
using FoodOrg.Common;
using FoodOrg.BLL;
using FoodOrg.Models;
using System.Threading;

namespace FoodService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        //service
        //payment.AllPay ws = new payment.AllPay();
        private static readonly System.Timers.Timer aTimer = new System.Timers.Timer();

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("FoodOrg CashFlowCheck Service", "Service Start", EventLogEntryType.Information, 201);
            /*定時執行*/
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(TimeEvent);
            // 設置時間間隔　設為一分鐘
            aTimer.Interval = 1000 * 60;
            aTimer.Enabled = true;
            aTimer.AutoReset = true;
        }

        protected override void OnStop()
        {
            //System.Timers.Timer aTimer = new System.Timers.Timer();
            EventLog.WriteEntry("FoodOrg CashFlowCheck Service", "Service Stop", EventLogEntryType.Information, 202);
            aTimer.Enabled = false;
            aTimer.AutoReset = false;
        }

        /// <summary>
        /// 當時間發生的時後需要進行的程式
        /// </summary>
        private void TimeEvent(object source, ElapsedEventArgs e)
        {
            ////主動檢查訂單是否已繳費
            //if (e.SignalTime.ToString("mm") == "00")
            //{
            //    CashFlowCheck1();
            //}


            //訂單檢查是否逾期
            if (e.SignalTime.ToString("HHmm") == "0010")
            {
                CashFlowCheck2();
            }


            //檢查會員是否已付過費用 如果已繳過則將status改為正常
            if (e.SignalTime.ToString("HHmm") == "0100")
            {
                MemberCheckStatus();
            }
        }

        /// <summary>
        /// 主動檢查訂單是否已繳費
        /// </summary>
        private void CashFlowCheck1()
        {
            //取得所有「報名未繳」資料
            List<MOrder> li = new BOrder().GetByCheck();

            int i = 1;
            foreach (MOrder mod in li)
            {
                if (i % 10 == 0)
                {
                    Thread.Sleep(10000);
                }
                else
                {
                    //只能重傳結果
                    SmesCheck(mod.TradeNo, mod.MerchantTradeNo);
                }
                Thread.Sleep(5000);

                i++;
            }
        }

        /// <summary>
        /// 訂單檢查是否逾期
        /// </summary>
        private void CashFlowCheck2()
        {
            EventLog.WriteEntry("FoodOrg CashFlowCheck Service", "Service Stop", EventLogEntryType.Information, 202);
            //取得所有「未繳」資料

            List<MOrder> li = new BOrder().GetByCheck();

            int i = 1;
            foreach (MOrder mod in li)
            {
                if (i % 10 == 0)
                {
                    Thread.Sleep(10000);
                }
                else
                {
                    //DateTime dtDeadLine = Convert.ToDateTime(mod.DeadlineDate).AddDays(3);
                    DateTime dtDeadLine = mod.DeadlineDate;
                    //判斷是否超過繳費期限
                    if (dtDeadLine < DateTime.Now)
                    {
                        //只能重傳結果
                        SmesCheck(mod.TradeNo, mod.MerchantTradeNo);
                    }
                }
                Thread.Sleep(5000);
                
                i++;
            }
        }

        /// <summary>
        /// 檢查會員是否已付過費用 如果已繳過則將status改為正常
        /// </summary>
        private void MemberCheckStatus()
        {
            int intTWYear = new BTWYear().GetTop1().TWYear;
            //找出所有待繳費的使用者
            //個人會員
            BMemberP bllP = new BMemberP();
            List<MMemberP> listP = bllP.GetListByWaitPay();
            foreach (MMemberP mod in listP)
            {
                if (mod.Status == "待繳費")
                {
                    if (new BOrder().CheckMemberFeePay(mod.MemberPID, "P") > 0 && new BOrder().CheckYearFeePay(mod.MemberPID, "P", mod.MemberClass, intTWYear) > 0)
                    {
                        mod.Status = "正常";
                        new BMemberP().Edit(mod);
                    }
                }
            }

            //團體會員
            BMemberG bllG = new BMemberG();
            List<MMemberG> listG = bllG.GetListByWaitPay();
            foreach (MMemberG mod in listG)
            {
                if (mod.Status == "待繳費")
                {
                    if (new BOrder().CheckMemberFeePay(mod.MemberGID, "G") > 0 && new BOrder().CheckYearFeePay(mod.MemberGID, "G", mod.MemberClass, intTWYear) > 0)
                    {
                        mod.Status = "正常";
                        new BMemberG().Edit(mod);
                    }
                }
            }

        }

        /// <summary>
        /// 訊航訂單查詢(無法查詢，只能重送資料)
        /// </summary>
        private void SmesCheck(string strTradeNo, string strMerchantTradeNo)
        {
            string targetUrl = "https://ssl.smse.com.tw/ezpos/roturl.asp?";

            System.Text.StringBuilder postData = new System.Text.StringBuilder();
            postData.Append("Dcvc=" + "3505");
            postData.Append("&");
            postData.Append("Rvg2c=" + "1");
            postData.Append("&");
            postData.Append("Data_id=" + strMerchantTradeNo);
            postData.Append("&");
            postData.Append("Smseid=" + strTradeNo);
            postData.Append("&");
            postData.Append("Types=url");

            string result = "";
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(targetUrl, postData.ToString());
                    result = HtmlResult;

                    string str = result.Trim().Replace("\r\n", "");
                    if (str.IndexOf("資料已返送完成") <= 0)
                    {
                        Util.SendMail("alex@anytech.com.tw", "Food service重送錯誤", targetUrl + "?" + postData.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("ILSI CashFlowCheck Service", "Service Error" + ex.ToString(), EventLogEntryType.Warning, 400);
                Util.SendMail("alex@anytech.com.tw", "ILSI Service Error", ex.ToString());
                //result = ex.ToString();


            }
        }

        /// <summary>
        /// post 後取得回傳資料
        /// </summary>
        private string PostAndGetResut(string targetUrl)
        {
            string result = "";

            HttpWebRequest request = HttpWebRequest.Create(targetUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            try
            {
                // 取得回應資料
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        result = HttpUtility.UrlDecode(sr.ReadToEnd(), System.Text.Encoding.UTF8);
                    }
                }
                return result;
                //Response.Write(result);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
