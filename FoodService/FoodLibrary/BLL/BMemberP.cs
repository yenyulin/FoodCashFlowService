using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FoodOrg;

namespace FoodOrg.BLL
{
    /// <summary>
    /// 商業邏輯層 MemberP
    /// </summary>
    public class BMemberP
    {
        public BMemberP() { }

        #region  基本方法

        /// <summary>
        /// 新增資料
        /// </summary>
        public string Add(Models.MMemberP mod)
        {
            return new DAL.DMemberP().Add(mod);
        }

        /// <summary>
        /// 修改資料
        /// </summary>
        public bool Edit(Models.MMemberP mod)
        {
            return new DAL.DMemberP().Edit(mod);
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        public bool Del(string strMemberPID)
        {
            return new DAL.DMemberP().Del(strMemberPID);
        }

        /// <summary>
        /// 取得單筆資料
        /// </summary>
        public Models.MMemberP GetModel(string strMemberPID)
        {
            return new DAL.DMemberP().GetModel(strMemberPID);
        }

        ///// <summary>
        ///// 取得全部資料
        ///// </summary>
        //public List<Models.MMemberP> GetList()
        //{
        //    return new DAL.DMemberP().GetList();
        //}

        #endregion

        #region  自訂方法

        /// <summary>
        /// 取得待繳費
        /// </summary>
        public List<Models.MMemberP> GetListByWaitPay()
        {
            return new DAL.DMemberP().GetListByWaitPay();
        }


        #endregion
    }
}
