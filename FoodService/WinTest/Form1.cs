using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using FoodOrg;
using FoodOrg.BLL;
using FoodOrg.Models;
using System.Net;
using System.IO;
using System.Web;
using System.Threading;

namespace WinTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MemberCheckStatus();
        }



        private void SmesCheck()
        {
            string SmseBusinessID = "0527";
            int intAmoun = 400;
            string strCKValueB = intAmoun.ToString("00000000");
            string strSmseid = "9_4_1_3699143"; //這是測試用的
            string strValueC = SmesIDCheck(strSmseid.Substring(strSmseid.Length - 4, 4));


            string strCheckValue = SmseBusinessID + strCKValueB + strValueC;

            int intCheckE = 0;
            int intCheckF = 0;
            for (int i = 0; i <= strCheckValue.Length - 1; i++)
            {
                string strValueCheck = strCheckValue.Substring(i, 1);

                if (i % 2 == 0)
                {
                    intCheckF += Convert.ToInt32(strValueCheck);
                }
                else
                {
                    intCheckE += Convert.ToInt32(strValueCheck);

                }
            }
            intCheckE = intCheckE * 3;
            intCheckF = intCheckF * 9;

            textBox1.Text = "0" + "  " + (intCheckE + intCheckF);
        }

        private string SmesIDCheck(string strID)
        {
            string strValue = "";
            for (int i = 0; i <= 3; i++)
            {
                string strValueCheck = strID.Substring(i, 1);
                if (IsInt(strValueCheck))
                {
                    strValue += strValueCheck;
                }
                else { strValue += 9; }

            }
            return strValue;
        }

        /// <summary>
        /// 驗證整數
        /// </summary>
        /// <param name="input">要驗證的字串</param>
        /// <returns>是否通過驗證</returns>
        public static bool IsInt(string strValue)
        {
            return new System.Text.RegularExpressions.Regex(@"^-?\d+$").IsMatch(strValue);
        }



        /// <summary>
        /// 當時間發生的時後需要進行的程式
        /// </summary>
        private void TimeEvent(object source, ElapsedEventArgs e)
        {
            ////提醒客戶
            if (e.SignalTime.ToString("HHmm") == "0030")
            {
                CashFlowCheck();
            }
        }


        private void CashFlowCheck()
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
                    DateTime dtDeadLine = Convert.ToDateTime(mod.DeadlineDate).AddDays(3);
                    //判斷是否超過繳費期限 (入帳時間最多3天)
                    if (dtDeadLine < DateTime.Now)
                    {

                        //只能重傳結果
                        SmesCheck(mod.TradeNo, mod.MerchantTradeNo);
                    }
                }
                Thread.Sleep(5000);
                //現在能分成兩個部份
                i++;
            }
            //AllPayCheck("1406171555482979");
            //textBox1.Text = "done";
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
                        //Util.SendMail("alex@anytech.com.tw", "ilsi service重送錯誤", targetUrl+ "?"+postData.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }

            textBox1.Text = result;
            //PostAndGetResut(targetUrl);

        }

        /// <summary>
        /// 檢查會員是否已付過費用 如果已繳過則將status改為正常
        /// </summary>
        private void MemberCheckStatus()
        {
            int intTWYear = new BTWYear().GetTop1().TWYear;
            //找出所有待繳費的使用者
            //個人會員
            BMemberP bllP=new BMemberP();
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
            BMemberG bllG=new BMemberG();
            List<MMemberG>listG=bllG.GetListByWaitPay();
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
