using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FoodOrg;

namespace FoodOrg.BLL
{
    /// <summary>
    /// 商業邏輯層 MemberG
    /// </summary>
    public class BMemberG
    {
        public BMemberG() { }

        #region  基本方法

        /// <summary>
        /// 新增資料
        /// </summary>
        public string Add(Models.MMemberG mod)
        {
            return new DAL.DMemberG().Add(mod);
        }

        /// <summary>
        /// 修改資料
        /// </summary>
        public bool Edit(Models.MMemberG mod)
        {
            return new DAL.DMemberG().Edit(mod);
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        public bool Del(string strMemberGID)
        {
            return new DAL.DMemberG().Del(strMemberGID);
        }

        /// <summary>
        /// 取得單筆資料
        /// </summary>
        public Models.MMemberG GetModel(string strMemberGID)
        {
            return new DAL.DMemberG().GetModel(strMemberGID);
        }

        #endregion

        #region  自訂方法

        /// <summary>
        /// 取得待繳費
        /// </summary>
        public List<Models.MMemberG> GetListByWaitPay()
        {
            return new DAL.DMemberG().GetListByWaitPay();
        }
        #endregion
    }
}
