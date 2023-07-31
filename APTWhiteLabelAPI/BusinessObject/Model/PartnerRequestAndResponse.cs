namespace APTWhiteLabelAPI.BusinessObject.Model
{
    public class PartnerRequestAndResponse
    {
    }

    public class GetBalanceRequest
    {
        public string user_id { get; set; }
        public string amount { get; set; }
        public string merchant_id { get; set; }
        public string order_id { get; set; }
        public string check_sum { get; set; }
    }
    public class GetBalanceResponse
    {
        public string user_id { get; set; }
        public string balance_status { get; set; }
        public string check_sum { get; set; }
    }
    public class CreditDebitRequest
    {
        public string type { get; set; }
        public string user_id { get; set; }
        public string merchant_id { get; set; }
        public string transaction_amount { get; set; }
        public string offer_price { get; set; }
        public string transaction_id { get; set; }
        public string particulars { get; set; }
        public string check_sum { get; set; }
    }
    public class CreditDebitResponse
    {
        public string status { get; set; }
        public string transaction_id { get; set; }
        public string reference_id { get; set; }
        public string check_sum { get; set; }
    }

    public class TransactionUpdateRequest
    {
        public string user_id { get; set; }
        public string merchant_id { get; set; }
        public string transaction_status { get; set; }      
        public string transaction_id { get; set; }
        public string pnr_no { get; set; }
        public string check_sum { get; set; }
    }
    public class TransactionUpdateResponse
    {
        public string user_id { get; set; }
        public string transaction_status { get; set; }
        public string transaction_id { get; set; }
        public string check_sum { get; set; }
    }
}
