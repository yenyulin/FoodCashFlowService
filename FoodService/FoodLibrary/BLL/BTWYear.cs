using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FoodOrg;

namespace FoodOrg.BLL
{
    /// <summary>
    /// 商業邏輯層 TWYear
    /// </summary>
    public class BTWYear
    {
        public BTWYear() { }

        #region  基本方法

        /// <summary>
        /// 新增資料
        /// </summary>
        public int Add(Models.MTWYear mod)
        {
            return new DAL.DTWYear().Add(mod);
        }

        /// <summary>
        /// 修改資料
        /// </summary>
        public bool Edit(Models.MTWYear mod)
        {
            return new DAL.DTWYear().Edit(mod);
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        public bool Del(int intTWYear)
        {
            return new DAL.DTWYear().Del(intTWYear);
        }

        /// <summary>
        /// 取得單筆資料
        /// </summary>
        public Models.MTWYear GetModel(int intTWYear)
        {
            return new DAL.DTWYear().GetModel(intTWYear);
        }

        /// <summary>
        /// 取得全部資料
        /// </summary>
        public List<Models.MTWYear> GetList()
        {
            return new DAL.DTWYear().GetList();
        }

        #endregion

        #region  自訂方法

        /// <summary>
        /// 取得最新一筆Time資料
        /// </summary>
        public Models.MTWYear GetTop1()
        {
            return new DAL.DTWYear().GetTop1();
        }

        /// <summary>
        /// 取得Photo用到的年份資料
        /// </summary>
        public List<Models.MTWYear> GetByPhoto()
        {
            return new DAL.DTWYear().GetByPhoto();
        }

        /// <summary>
        /// 取得Paper用到的年份資料   bolIsFront=true 會找出Paper的PaperNo不為null的年份
        /// </summary>
        public List<Models.MTWYear> GetByPaperTime(bool bolIsFront)
        {
            return new DAL.DTWYear().GetByPaperTime(bolIsFront);
        }
        #endregion
    }
}
