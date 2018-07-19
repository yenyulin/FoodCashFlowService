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
    /// 資料存取層 TWYear
    /// </summary>
    public class DTWYear
    {
        public DTWYear() { }

        #region 基本方法

        /// <summary>
        /// 新增資料
        /// </summary>
        public int Add(Models.MTWYear mod)
        {
            SqlCommand cmd = new SqlCommand("STP_TWYearAdd");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TWYear", SqlDbType.Int).Value = mod.TWYear;
            cmd.Parameters.Add("@PeriodID", SqlDbType.Int).Value = mod.PeriodID;
            cmd.Parameters.Add("@PaperDateBegin", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.PaperDateBegin);
            cmd.Parameters.Add("@PaperDateEnd", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.PaperDateEnd);
            cmd.Parameters.Add("@RegisterDateBegin", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.RegisterDateBegin);
            cmd.Parameters.Add("@RegisterDateEnd", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.RegisterDateEnd);
            cmd.Parameters.Add("@PayDateBegin", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.PayDateBegin);
            cmd.Parameters.Add("@PayDateEnd", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.PayDateEnd);
            return SQLUtil.ExecuteSql(cmd);
        }
        /// <summary>
        /// 修改資料
        /// <summary>
        public bool Edit(Models.MTWYear mod)
        {
            SqlCommand cmd = new SqlCommand("STP_TWYearEdit");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TWYear", SqlDbType.Int).Value = mod.TWYear;
            cmd.Parameters.Add("@PeriodID", SqlDbType.Int).Value = mod.PeriodID;
            cmd.Parameters.Add("@PaperDateBegin", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.PaperDateBegin);
            cmd.Parameters.Add("@PaperDateEnd", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.PaperDateEnd);
            cmd.Parameters.Add("@RegisterDateBegin", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.RegisterDateBegin);
            cmd.Parameters.Add("@RegisterDateEnd", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.RegisterDateEnd);
            cmd.Parameters.Add("@PayDateBegin", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.PayDateBegin);
            cmd.Parameters.Add("@PayDateEnd", SqlDbType.DateTime).Value = SQLUtil.CheckDBValue(mod.PayDateEnd);
            return SQLUtil.ExecuteSql(cmd) > 0;
        }

        /// <summary>
        /// 刪除資料
        /// <summary>
        public bool Del(int intTWYear)
        {
            SqlCommand cmd = new SqlCommand("STP_TWYearDel");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TWYear", SqlDbType.Int).Value = intTWYear;
            return SQLUtil.ExecuteSql(cmd) > 0;
        }

        /// <summary>
        /// 取得單筆資料
        /// <summary>
        public Models.MTWYear GetModel(int intTWYear)
        {
            SqlCommand cmd = new SqlCommand("STP_TWYearGetByPK");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TWYear", SqlDbType.Int).Value = intTWYear;
            SqlDataReader dr = SQLUtil.QueryDR(cmd);
            bool isHasRows = dr.HasRows;
            Models.MTWYear mod = SetModel(dr);
            dr.Close();
            if (isHasRows)
            {
                return mod;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取得所有資料
        /// </summary>
        public List<Models.MTWYear> GetList()
        {
            SqlCommand cmd = new SqlCommand("STP_TWYearGet");
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = SQLUtil.QueryDS(cmd);
            return GetList(ds);
        }

        /// <summary>
        /// 實體物件取得DataReader資料
        /// </summary>
        private Models.MTWYear SetModel(SqlDataReader dr)
        {
            Models.MTWYear mod = new Models.MTWYear();
            while (dr.Read())
            {
                mod.TWYear = int.Parse(dr["TWYear"].ToString());
                mod.PeriodID = int.Parse(dr["PeriodID"].ToString());
                mod.PaperDateBegin = SQLUtil.GetDateTime(dr["PaperDateBegin"]);
                mod.PaperDateEnd = SQLUtil.GetDateTime(dr["PaperDateEnd"]);
                mod.RegisterDateBegin = SQLUtil.GetDateTime(dr["RegisterDateBegin"]);
                mod.RegisterDateEnd = SQLUtil.GetDateTime(dr["RegisterDateEnd"]);
                mod.PayDateBegin = SQLUtil.GetDateTime(dr["PayDateBegin"]);
                mod.PayDateEnd = SQLUtil.GetDateTime(dr["PayDateEnd"]);
            }
            return mod;
        }

        /// <summary>
        /// 實體物件取得DataRow資料
        /// </summary>
        private Models.MTWYear SetModel(DataRow dr)
        {
            Models.MTWYear mod = new Models.MTWYear();
            mod.TWYear = int.Parse(dr["TWYear"].ToString());
            mod.PeriodID = int.Parse(dr["PeriodID"].ToString());
            mod.PaperDateBegin = SQLUtil.GetDateTime(dr["PaperDateBegin"]);
            mod.PaperDateEnd = SQLUtil.GetDateTime(dr["PaperDateEnd"]);
            mod.RegisterDateBegin = SQLUtil.GetDateTime(dr["RegisterDateBegin"]);
            mod.RegisterDateEnd = SQLUtil.GetDateTime(dr["RegisterDateEnd"]);
            mod.PayDateBegin = SQLUtil.GetDateTime(dr["PayDateBegin"]);
            mod.PayDateEnd = SQLUtil.GetDateTime(dr["PayDateEnd"]);
            return mod;
        }


        /// <summary>
        /// 由DataSet取得泛型資料列表
        /// </summary>
        private List<Models.MTWYear> GetList(DataSet ds)
        {
            List<Models.MTWYear> li = new List<Models.MTWYear>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                li.Add(SetModel(dr));
            }
            return li;
        }

        #endregion

        #region  自訂方法

        /// <summary>
        /// 取得最新一筆Time資料
        /// <summary>
        public Models.MTWYear GetTop1()
        {
            SqlCommand cmd = new SqlCommand("STP_TWYearGetTop1");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = SQLUtil.QueryDR(cmd);
            bool isHasRows = dr.HasRows;
            Models.MTWYear mod = SetModel(dr);
            dr.Close();
            if (isHasRows)
            {
                return mod;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取得Photo用到的年份資料
        /// </summary>
        public List<Models.MTWYear> GetByPhoto()
        {
            SqlCommand cmd = new SqlCommand("STP_TWYearGetByPhoto");
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = SQLUtil.QueryDS(cmd);
            return GetList(ds);
        }

        /// <summary>
        /// 取得Paper用到的年份資料   bolIsFront=true 會找出Paper的PaperNo不為null的年份
        /// </summary>
        public List<Models.MTWYear> GetByPaperTime( bool bolIsFront)
        {
            SqlCommand cmd = new SqlCommand("STP_TWYearGetByPaper");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@IsFront", SqlDbType.Bit).Value = bolIsFront;
            DataSet ds = SQLUtil.QueryDS(cmd);
            return GetList(ds);
        }
        #endregion
    }
}
