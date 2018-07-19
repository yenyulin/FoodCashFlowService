using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FoodOrg;

namespace FoodOrg.BLL
{
    /// <summary>
    /// 商業邏輯層 Order
    /// </summary>
    public class BOrder
    {
        public BOrder() { }

        #region  基本方法


        #endregion

        #region  自訂方法

        /// <summary>
        /// 取得所有需要驗證的資料
        /// </summary>
        public List<Models.MOrder> GetByCheck()
        {
            return new DAL.DOrder().GetByCheck();
        }

        /// <summary>
        /// 是否繳過入會費
        /// </summary>
        public int CheckMemberFeePay(string strMemberID, string strMemberType)
        {
            return new DAL.DOrder().CheckMemberFeePay(strMemberID, strMemberType);
        }

        /// <summary>
        /// 是否繳過常年會費
        /// </summary>
        public int CheckYearFeePay(string strMemberID, string strMemberType, string strMemberClass, int intTWYear)
        {
            return new DAL.DOrder().CheckYearFeePay(strMemberID, strMemberType, strMemberClass, intTWYear);
        }

        ///// <summary>
        ///// 是否繳過年會出席會
        ///// </summary>
        //public int CheckAttendFeePay(string strMemberID, string strMemberType, int intTWYear)
        //{
        //    return new DAL.DOrder().CheckAttendFeePay(strMemberID, strMemberType, intTWYear);
        //}



        #endregion
    }
}
