using APTWhiteLabelAPI.BusinessObject.Model;
using System.Data;
using APTWhiteLabelAPI.DataAccessLayer;
using APTWhiteLabelAPI.BusinessLayer;
using System.Security.Cryptography;
using System.Text;

namespace APTWhiteLabelAPI.BusinessLayer
{
    public class TravelAPI
    {
        private readonly string _saltKey;
        public static IConfiguration Configuration { get; set; }
        public static LogServicecs _log = new LogServicecs();

        TravelRepository tr = new TravelRepository();
        public TravelAPI()
        {
            Configuration = GetConfiguration();
            _saltKey = Configuration["TravelKeys:SaltKey"];
        }
        public IConfiguration GetConfiguration()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            return Configuration;
        }
        public GetBalanceResponse GetBalance(GetBalanceRequest gbrq)
        {
            GetBalanceResponse gb = new GetBalanceResponse();
            _log.WriteLog("GetBalance", "request", "", gbrq.check_sum);
            try
            {
                string chvalue = gbrq.user_id + gbrq.merchant_id + gbrq.order_id + gbrq.amount + _saltKey;
                chvalue = ComputeSHA512(chvalue);
                if (chvalue != gbrq.check_sum)
                {
                    gb.balance_status = "CheckSum Value Not matched";
                    gb.check_sum = gbrq.check_sum;
                    gb.user_id = gbrq.user_id;
                    return gb;
                }

                DataSet dst = tr.GetBalance(gbrq);
                if (dst != null)
                {
                    if (dst.Tables.Count > 0)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "100")
                            {
                                gb.balance_status = "true";
                                gb.check_sum = gbrq.check_sum;
                                gb.user_id = gbrq.user_id;
                            }
                            else
                            {
                                gb.balance_status = "false";
                                gb.check_sum = gbrq.check_sum;
                                gb.user_id = gbrq.user_id;
                            }
                        }
                        else
                        {
                            gb.balance_status = "false";
                            gb.check_sum = gbrq.check_sum;
                            gb.user_id = gbrq.user_id;
                        }
                    }
                    else
                    {
                        gb.balance_status = "false";
                        gb.check_sum = gbrq.check_sum;
                        gb.user_id = gbrq.user_id;
                    }
                }
                else
                {
                    gb.balance_status = "false";
                    gb.check_sum = gbrq.check_sum;
                    gb.user_id = gbrq.user_id;
                }
            }
            catch (Exception ex)
            {

            }
            return gb;
        }
        public CreditDebitResponse Credit(CreditDebitRequest cdrq)
        {
            CreditDebitResponse gb = new CreditDebitResponse();
            try
            {
                string chvalue = cdrq.type + cdrq.user_id + cdrq.merchant_id + cdrq.transaction_amount + cdrq.transaction_id + cdrq.particulars + _saltKey;
                chvalue = ComputeSHA512(chvalue);
                if (chvalue != cdrq.check_sum)
                {
                    gb.status = "CheckSum Value Not matched";
                    gb.check_sum = cdrq.check_sum;
                    gb.reference_id = "";
                    gb.transaction_id = "";
                    return gb;
                }
                DataSet dst = tr.CreditDebitBalanceBalance(cdrq, 2);
                if (dst != null)
                {
                    if (dst.Tables.Count > 0)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "100")
                            {
                                gb.status = "true";
                                gb.check_sum = cdrq.check_sum;
                                gb.reference_id = "";
                                gb.transaction_id = dst.Tables[0].Rows[0][2].ToString();
                            }
                        }
                        else
                        {
                            gb.status = "false";
                            gb.check_sum = cdrq.check_sum;
                            gb.reference_id = "";
                            gb.transaction_id = cdrq.transaction_id;
                        }
                    }
                    else
                    {
                        gb.status = "false";
                        gb.check_sum = cdrq.check_sum;
                        gb.reference_id = "";
                        gb.transaction_id = cdrq.transaction_id;
                    }
                }
                else
                {
                    gb.status = "false";
                    gb.check_sum = cdrq.check_sum;
                    gb.reference_id = "";
                    gb.transaction_id = cdrq.transaction_id;
                }
            }
            catch (Exception ex)
            {

            }
            return gb;
        }
        public CreditDebitResponse Debit(CreditDebitRequest cdrq)
        {
            CreditDebitResponse gb = new CreditDebitResponse();
            try
            {
                string chvalue = cdrq.type + cdrq.user_id + cdrq.merchant_id + cdrq.transaction_amount + cdrq.transaction_id + cdrq.particulars + _saltKey;
                chvalue = ComputeSHA512(chvalue);
                if (chvalue != cdrq.check_sum)
                {
                    gb.status = "CheckSum Value Not matched";
                    gb.check_sum = cdrq.check_sum;
                    gb.reference_id = "";
                    gb.transaction_id = "";
                    return gb;
                }
                DataSet dst = tr.CreditDebitBalanceBalance(cdrq, 1);
                if (dst != null)
                {
                    if (dst.Tables.Count > 0)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "100")
                            {
                                gb.status = "true";
                                gb.check_sum = cdrq.check_sum;
                                gb.reference_id = "";
                                gb.transaction_id = dst.Tables[0].Rows[0][2].ToString();
                            }
                        }
                        else
                        {
                            gb.status = "false";
                            gb.check_sum = cdrq.check_sum;
                            gb.reference_id = "";
                            gb.transaction_id = cdrq.transaction_id;
                        }
                    }
                    else
                    {
                        gb.status = "false";
                        gb.check_sum = cdrq.check_sum;
                        gb.reference_id = "";
                        gb.transaction_id = cdrq.transaction_id;
                    }
                }
                else
                {
                    gb.status = "false";
                    gb.check_sum = cdrq.check_sum;
                    gb.reference_id = "";
                    gb.transaction_id = cdrq.transaction_id;
                }
            }
            catch (Exception ex)
            {

            }
            return gb;
        }
        public TransactionUpdateResponse TransactionUpdate(TransactionUpdateRequest turq)
        {
            TransactionUpdateResponse gb = new TransactionUpdateResponse();
            try
            {
                string chvalue = turq.user_id + turq.merchant_id + turq.transaction_id + turq.pnr_no + _saltKey;
                chvalue = ComputeSHA512(chvalue);
                if (chvalue != turq.check_sum)
                {
                    gb.transaction_status = "CheckSum Value Not matched";
                    gb.check_sum = turq.check_sum;
                    gb.user_id = turq.user_id;
                    gb.transaction_id = turq.transaction_id;
                    return gb;
                }
                DataSet dst = tr.TransactionUpdate(turq);
                if (dst != null)
                {
                    if (dst.Tables.Count > 0)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            if (dst.Tables[0].Rows[0][0].ToString() == "100")
                            {
                                gb.transaction_status = dst.Tables[0].Rows[0][1].ToString();
                                gb.check_sum = turq.check_sum;
                                gb.user_id = turq.user_id;
                                gb.transaction_id = turq.transaction_id;
                            }
                            else
                            {
                                gb.transaction_status = dst.Tables[0].Rows[0][1].ToString();
                                gb.check_sum = turq.check_sum;
                                gb.user_id = turq.user_id;
                                gb.transaction_id = turq.transaction_id;
                            }
                        }
                        else
                        {
                            gb.transaction_status = "Failed";
                            gb.check_sum = turq.check_sum;
                            gb.user_id = turq.user_id;
                            gb.transaction_id = turq.transaction_id;
                        }
                    }
                    else
                    {
                        gb.transaction_status = "Failed";
                        gb.check_sum = turq.check_sum;
                        gb.user_id = turq.user_id;
                        gb.transaction_id = turq.transaction_id;
                    }
                }
                else
                {
                    gb.transaction_status = "Failed";
                    gb.check_sum = turq.check_sum;
                    gb.user_id = turq.user_id;
                    gb.transaction_id = turq.transaction_id;
                }
            }
            catch (Exception ex)
            {

            }
            return gb;
        }

        public string ComputeSHA512(string chvalue)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                using (SHA512 sha512 = SHA512.Create())
                {
                    byte[] hashValue = sha512.ComputeHash(Encoding.UTF8.GetBytes(chvalue));
                    foreach (byte b in hashValue)
                    {
                        sb.Append($"{b:X2}");
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return sb.ToString();
            }
        }
    }
}
