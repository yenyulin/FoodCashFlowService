using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Net.Mail;
using FoodOrg.Models;
using FoodOrg.BLL;
namespace FoodOrg.Common
{
    /// <summary>
    /// 工具類
    /// </summary>
    public class Util
    {
        public static readonly string ProjectName = "FoodOrg";



        public static bool SmesCheck()
        {
            bool isSend = true;
            return isSend;
        }



        /// <summary>
        /// 發送Email
        /// </summary>
        /// <param name="strTo">收件人</param>
        /// <param name="strSubject">標題</param>
        /// <param name="strBody">內容</param>
        /// <returns>是否發送成功</returns>
        public static bool SendMail(string strTo, string strSubject, string strBody)
        {
            bool isSend = true;
            try
            {
                MailAddress maFrom = new MailAddress("tafst@food.org.tw");
                MailAddress maTo = new MailAddress(strTo);
                //MailAddress maTo = new MailAddress("kerwinlo@hotmail.com");
                MailMessage mm = new MailMessage(maFrom, maTo);
                mm.Subject = strSubject;
                mm.SubjectEncoding = System.Text.Encoding.UTF8;
                mm.Body = strBody;
                mm.BodyEncoding = System.Text.Encoding.UTF8;
                mm.IsBodyHtml = true;
                SmtpClient sc = new SmtpClient("vmail.fetnet.net", 587);
                sc.Credentials = new System.Net.NetworkCredential("tafst@food.org.tw", "swr987415");
                sc.Port = 587;
                //sc.Port = 465;
                sc.EnableSsl = true;
                sc.Send(mm);
            }
            catch (Exception ex)
            {
                isSend = false;
                throw ex;
            }
            return isSend;
        }

        #region  Control To Model

        /// <summary>
        /// String轉換為Null
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static string GetStringNullable(string strValue)
        {
            if (strValue == "")
            {
                strValue = null;
            }
            return strValue;
        }

        /// <summary>
        /// String轉換為int
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static int GetInt(string strValue)
        {
            int intValue;
            if (int.TryParse(strValue, out intValue))
            {
                return intValue;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// String轉換為int(Nullable)
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static int? GetIntNullable(string strValue)
        {
            int intValue;
            if (int.TryParse(strValue, out intValue))
            {
                return intValue;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// String轉換為Decimal
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static decimal GetDecimal(string strValue)
        {
            Decimal decValue;
            if (Decimal.TryParse(strValue, out decValue))
            {
                return decValue;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// String轉換為Decimal(Nullable)
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static Decimal? GetDecimalNullable(string strValue)
        {
            Decimal decValue;
            if (Decimal.TryParse(strValue, out decValue))
            {
                return decValue;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// string型轉換為DateTime型
        /// </summary>
        /// <param name="strValue">轉換後的結果</param>
        /// <returns>轉換後的結果</returns>
        public static DateTime GetDateTime(string strValue)
        {
            strValue = strValue.Trim();

            if (strValue != "")
            {
                return DateTime.Parse(strValue);
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// String轉換為DateTime(Nullable)
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static DateTime? GetDateTimeNullable(string strValue)
        {
            DateTime dt;
            if (strValue != null && DateTime.TryParse(strValue, out dt))
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// String轉換為bool
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static bool GetBool(string strValue)
        {
            bool boolValue;
            if (bool.TryParse(strValue, out boolValue))
            {
                return boolValue;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// String轉換為bool(Nullable)
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static bool? GetBoolNullable(string strValue)
        {
            bool boolValue;
            if (bool.TryParse(strValue, out boolValue))
            {
                return boolValue;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region  Model To Control

        /// <summary>
        /// int轉換為String
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static string SetInt(int intValue)
        {
            return intValue.ToString();
        }

        /// <summary>
        /// int轉換為String(Nullable)
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static string SetIntNullable(int? intValue)
        {
            if (intValue.HasValue)
            {
                return intValue.Value.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Decimal轉換為String
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static string SetDecimal(Decimal decValue, int intDecimalPoint)
        {
            string strDecimalPoint = "";
            int i = 0;
            while (i < intDecimalPoint)
            {
                strDecimalPoint += "#";
                i++;
            }
            if (strDecimalPoint.Length > 0)
            {
                strDecimalPoint = "." + strDecimalPoint;
            }

            return decValue.ToString("0." + strDecimalPoint);
        }

        /// <summary>
        /// Decimal轉換為String(Nullable)
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static string SetDecimalNullable(Decimal? decValue, int intDecimalPoint)
        {
            if (decValue.HasValue)
            {
                string strDecimalPoint = "";
                int i = 0;
                while (i < intDecimalPoint)
                {
                    strDecimalPoint += "#";
                    i++;
                }
                if (strDecimalPoint.Length > 0)
                {
                    strDecimalPoint = "." + strDecimalPoint;
                }

                return decValue.Value.ToString("0." + strDecimalPoint);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// DateTime轉換為String
        /// </summary>
        /// <param name="strValue">要轉換的DateTime</param>
        /// <returns>轉換後的String結果</returns>
        public static string SetDateTime(DateTime dtValue)
        {
            return dtValue.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// DateTime轉換為String(Nullable)
        /// </summary>
        /// <param name="strValue">要轉換的DateTime</param>
        /// <returns>轉換後的String結果</returns>
        public static string SetDateTimeNullable(DateTime? dtValue)
        {
            if (dtValue.HasValue)
            {
                return dtValue.Value.ToString("yyyy/MM/dd");
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// DateTime轉換為String(Nullable)
        /// </summary>
        /// <param name="strValue">要轉換的DateTime</param>
        /// <returns>轉換後的String結果</returns>
        public static string SetLongDateTimeNullable(DateTime? dtValue)
        {
            if (dtValue.HasValue)
            {
                return dtValue.Value.ToString("yyyy/MM/dd HH:ss:mm");
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// String轉換為bool
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static string SetBoolNullable(bool boolValue)
        {
            return boolValue.ToString();
        }
       
        /// <summary>
        /// String轉換為bool(Nullable)
        /// </summary>
        /// <param name="strValue">要轉換的值</param>
        /// <returns>轉換後的結果</returns>
        public static string SetBoolNullable(bool? boolValue)
        {
            if (boolValue.HasValue)
            {
                return boolValue.ToString();
            }
            else
            {
                return "";
            }
        }

        #endregion
    }
}