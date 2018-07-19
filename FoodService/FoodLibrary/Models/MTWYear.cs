using System;

namespace FoodOrg.Models
{
    /// <summary>
    /// TWYear
    /// </summary>
    public class MTWYear
    {
        public MTWYear() { }

        private int _TWYear;
        private int _PeriodID;
        private DateTime? _PaperDateBegin;
        private DateTime? _PaperDateEnd;
        private DateTime? _RegisterDateBegin;
        private DateTime? _RegisterDateEnd;
        private DateTime? _PayDateBegin;
        private DateTime? _PayDateEnd;

        /// <summary>
        /// 屆
        /// </summary>
        public int TWYear
        {
            set { _TWYear = value; }
            get { return _TWYear; }
        }

        /// <summary>
        /// 年
        /// </summary>
        public int PeriodID
        {
            set { _PeriodID = value; }
            get { return _PeriodID; }
        }

        /// <summary>
        /// 論文投稿開始日期
        /// </summary>
        public DateTime? PaperDateBegin
        {
            set { _PaperDateBegin = value; }
            get { return _PaperDateBegin; }
        }

        /// <summary>
        /// 論文投稿結束日期
        /// </summary>
        public DateTime? PaperDateEnd
        {
            set { _PaperDateEnd = value; }
            get { return _PaperDateEnd; }
        }

        /// <summary>
        /// 線上報名開始日期
        /// </summary>
        public DateTime? RegisterDateBegin
        {
            set { _RegisterDateBegin = value; }
            get { return _RegisterDateBegin; }
        }

        /// <summary>
        /// 線上報名結束日期
        /// </summary>
        public DateTime? RegisterDateEnd
        {
            set { _RegisterDateEnd = value; }
            get { return _RegisterDateEnd; }
        }

        /// <summary>
        /// 線上繳費開始日期
        /// </summary>
        public DateTime? PayDateBegin
        {
            set { _PayDateBegin = value; }
            get { return _PayDateBegin; }
        }

        /// <summary>
        /// 線上繳費結束日期
        /// </summary>
        public DateTime? PayDateEnd
        {
            set { _PayDateEnd = value; }
            get { return _PayDateEnd; }
        }

    }
}
