using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using FoodOrg;
using FoodOrg.Common;

namespace FoodOrg.DAL
{
    /// <summary>
    /// 資料存取層 Order
    /// </summary>
    public class DOrder
    {
        public DOrder() { }

        #region 基本方法


        /// <summary>
        /// 實體物件取得DataReader資料
        /// </summary>
        private Models.MOrder SetModel(SqlDataReader dr)
        {
            Models.MOrder mod = new Models.MOrder();
            while (dr.Read())
            {
                mod.OrderID = int.Parse(dr["OrderID"].ToString());
                mod.TWYear = int.Parse(dr["TWYear"].ToString());
                mod.MemberGID = dr["MemberGID"].ToString();
                mod.MemberPID = dr["MemberPID"].ToString();
                mod.MerchantTradeNo = dr["MerchantTradeNo"].ToString();
                mod.TradeNo = dr["TradeNo"].ToString();
                mod.TradeAmount = int.Parse(dr["TradeAmount"].ToString());
                mod.PaymentType = dr["PaymentType"].ToString();
                mod.Payment1 = dr["Payment1"].ToString();
                mod.Payment2 = dr["Payment2"].ToString();
                mod.Payment3 = dr["Payment3"].ToString();
                mod.PaymentNo = dr["PaymentNo"].ToString();
                mod.WebATMAccBank = dr["WebATMAccBank"].ToString();
                mod.TradeDate = DateTime.Parse(dr["TradeDate"].ToString());
                mod.DeadlineDate = DateTime.Parse(dr["DeadlineDate"].ToString());
                mod.PayDate = SQLUtil.GetDateTime(dr["PayDate"]);
                mod.RecDate = SQLUtil.GetDateTime(dr["RecDate"]);
                mod.PayFrom = dr["PayFrom"].ToString();
                mod.FeeStatus = dr["FeeStatus"].ToString();
                mod.Remark = dr["Remark"].ToString();
            }
            return mod;
        }

        /// <summary>
        /// 實體物件取得DataRow資料
        /// </summary>
        private Models.MOrder SetModel(DataRow dr)
        {
            Models.MOrder mod = new Models.MOrder();
            mod.OrderID = int.Parse(dr["OrderID"].ToString());
            mod.TWYear = int.Parse(dr["TWYear"].ToString());
            mod.MemberGID = dr["MemberGID"].ToString();
            mod.MemberPID = dr["MemberPID"].ToString();
            mod.MerchantTradeNo = dr["MerchantTradeNo"].ToString();
            mod.TradeNo = dr["TradeNo"].ToString();
            mod.TradeAmount = int.Parse(dr["TradeAmount"].ToString());
            mod.PaymentType = dr["PaymentType"].ToString();
            mod.Payment1 = dr["Payment1"].ToString();
            mod.Payment2 = dr["Payment2"].ToString();
            mod.Payment3 = dr["Payment3"].ToString();
            mod.PaymentNo = dr["PaymentNo"].ToString();
            mod.WebATMAccBank = dr["WebATMAccBank"].ToString();
            mod.TradeDate = DateTime.Parse(dr["TradeDate"].ToString());
            mod.DeadlineDate = DateTime.Parse(dr["DeadlineDate"].ToString());
            mod.PayDate = SQLUtil.GetDateTime(dr["PayDate"]);
            mod.RecDate = SQLUtil.GetDateTime(dr["RecDate"]);
            mod.PayFrom = dr["PayFrom"].ToString();
            mod.FeeStatus = dr["FeeStatus"].ToString();
            mod.Remark = dr["Remark"].ToString();
            return mod;
        }


        /// <summary>
        /// 由DataSet取得泛型資料列表
        /// </summary>
        private List<Models.MOrder> GetList(DataSet ds)
        {
            List<Models.MOrder> li = new List<Models.MOrder>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                li.Add(SetModel(dr));
            }
            return li;
        }

        #endregion

        #region  自訂方法

        /// <summary>
        /// 取得所有需要驗證的資料
        /// </summary>
        public List<Models.MOrder> GetByCheck()
        {
            SqlCommand cmd = new SqlCommand("STP_OrderGetByCheck");
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = SQLUtil.QueryDS(cmd);
            return GetList(ds);
        }

        /// <summary>
        /// 是否繳過入會費
        /// <summary>
        public int CheckMemberFeePay(string strMemberID, string strMemberType)
        {
            SqlCommand cmd = new SqlCommand("STP_FeeCheckMemberPay");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MemberType", SqlDbType.NVarChar).Value = strMemberType;
            cmd.Parameters.Add("@MemberID", SqlDbType.NVarChar).Value = strMemberID;
            object obj = SQLUtil.ExecuteScalar(cmd);
            int intCount = 0;
            if (obj != null && int.TryParse(obj.ToString(), out intCount))
            {
                intCount = Convert.ToInt32(obj.ToString());
            }
            return intCount;
        }

        /// <summary>
        /// 是否繳過常年會費
        /// <summary>
        public int CheckYearFeePay(string strMemberID, string strMemberType, string strMemberClass, int intTWYear)
        {
            SqlCommand cmd = new SqlCommand("STP_FeeCheckYearPay");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MemberType", SqlDbType.NVarChar).Value = strMemberType;
            cmd.Parameters.Add("@MemberID", SqlDbType.NVarChar).Value = strMemberID;
            cmd.Parameters.Add("@MemberClass", SqlDbType.NVarChar).Value = strMemberClass;
            cmd.Parameters.Add("@TWYear", SqlDbType.Int).Value = intTWYear;
            object obj = SQLUtil.ExecuteScalar(cmd);
            int intCount = 0;
            if (obj != null && int.TryParse(obj.ToString(), out intCount))
            {
                intCount = Convert.ToInt32(obj.ToString());
            }
            return intCount;
        }

        ///// <summary>
        ///// 是否繳過年會出席會
        ///// <summary>
        //public int CheckAttendFeePay(string strMemberID, string strMemberType, int intTWYear)
        //{
        //    SqlCommand cmd = new SqlCommand("STP_OrderCheckAttendPay");
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@MemberType", SqlDbType.NVarChar).Value = strMemberType;
        //    cmd.Parameters.Add("@MemberID", SqlDbType.NVarChar).Value = strMemberID;
        //    cmd.Parameters.Add("@TWYear", SqlDbType.Int).Value = intTWYear;
        //    object obj = SQLUtil.ExecuteScalar(cmd);
        //    int intCount = 0;
        //    if (obj != null && int.TryParse(obj.ToString(), out intCount))
        //    {
        //        intCount = Convert.ToInt32(obj.ToString());
        //    }
        //    return intCount;
        //}

        #endregion
    }
}
