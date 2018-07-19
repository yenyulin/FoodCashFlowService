using System;

namespace FoodOrg.Models
{
    /// <summary>
    /// Order
    /// </summary>
    public class MOrder
    {
        public MOrder() { }

        private int _OrderID;
        private int _TWYear;
        private string _MemberGID;
        private string _MemberPID;
        private string _MerchantTradeNo;
        private string _TradeNo;
        private int _TradeAmount;
        private string _PaymentType;
        private string _Payment1;
        private string _Payment2;
        private string _Payment3;
        private string _PaymentNo;
        private string _WebATMAccBank;
        private DateTime _TradeDate;
        private DateTime _DeadlineDate;
        private DateTime? _PayDate;
        private DateTime? _RecDate;
        private string _PayFrom;
        private string _FeeStatus;
        private string _Remark;

        /// <summary>
        /// 金流繳費紀錄序號
        /// </summary>
        public int OrderID
        {
            set { _OrderID = value; }
            get { return _OrderID; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TWYear
        {
            set { _TWYear = value; }
            get { return _TWYear; }
        }

        /// <summary>
        /// 會籍編號
        /// </summary>
        public string MemberGID
        {
            set { _MemberGID = value; }
            get { return _MemberGID; }
        }

        /// <summary>
        /// 會籍編號
        /// </summary>
        public string MemberPID
        {
            set { _MemberPID = value; }
            get { return _MemberPID; }
        }

        /// <summary>
        /// 交易自定編號
        /// </summary>
        public string MerchantTradeNo
        {
            set { _MerchantTradeNo = value; }
            get { return _MerchantTradeNo; }
        }

        /// <summary>
        /// smilepay交易編號
        /// </summary>
        public string TradeNo
        {
            set { _TradeNo = value; }
            get { return _TradeNo; }
        }

        /// <summary>
        /// 繳費金額
        /// </summary>
        public int TradeAmount
        {
            set { _TradeAmount = value; }
            get { return _TradeAmount; }
        }

        /// <summary>
        /// 繳費方式 1.WebATM  2.超商代碼-7-11 3.超商代碼-其它   4.超商條碼
        /// </summary>
        public string PaymentType
        {
            set { _PaymentType = value; }
            get { return _PaymentType; }
        }

        /// <summary>
        /// 超商條碼1
        /// </summary>
        public string Payment1
        {
            set { _Payment1 = value; }
            get { return _Payment1; }
        }

        /// <summary>
        /// 超商條碼2
        /// </summary>
        public string Payment2
        {
            set { _Payment2 = value; }
            get { return _Payment2; }
        }

        /// <summary>
        /// 超商條碼3
        /// </summary>
        public string Payment3
        {
            set { _Payment3 = value; }
            get { return _Payment3; }
        }

        /// <summary>
        /// 繳費代碼
        /// </summary>
        public string PaymentNo
        {
            set { _PaymentNo = value; }
            get { return _PaymentNo; }
        }

        /// <summary>
        /// 付款人銀行代碼
        /// </summary>
        public string WebATMAccBank
        {
            set { _WebATMAccBank = value; }
            get { return _WebATMAccBank; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TradeDate
        {
            set { _TradeDate = value; }
            get { return _TradeDate; }
        }

        /// <summary>
        /// 到期時間
        /// </summary>
        public DateTime DeadlineDate
        {
            set { _DeadlineDate = value; }
            get { return _DeadlineDate; }
        }

        /// <summary>
        /// 付款時間
        /// </summary>
        public DateTime? PayDate
        {
            set { _PayDate = value; }
            get { return _PayDate; }
        }

        /// <summary>
        /// 接收時間
        /// </summary>
        public DateTime? RecDate
        {
            set { _RecDate = value; }
            get { return _RecDate; }
        }

        /// <summary>
        /// 繳費超商
        /// </summary>
        public string PayFrom
        {
            set { _PayFrom = value; }
            get { return _PayFrom; }
        }

        /// <summary>
        /// 付款狀態 1.待繳費 2.已繳費 3.逾期 4.取消
        /// </summary>
        public string FeeStatus
        {
            set { _FeeStatus = value; }
            get { return _FeeStatus; }
        }

        /// <summary>
        /// 備註
        /// </summary>
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }

    }
}
