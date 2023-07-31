
using APTWhiteLabelAPI.BusinessLayer;
using APTWhiteLabelAPI.BusinessObject.Model;
using System.Data;
using System.Data.SqlClient;


namespace APTWhiteLabelAPI.DataAccessLayer
{
    public class TravelRepository
    {
        private readonly string _connString;
        private readonly string _txnconnString;
        private readonly string _masterconnString;
        private readonly string _WhitelabelconnString;

        public static IConfiguration Configuration { get; set; }
        public static LogServicecs LogServicecs { get; set; }

        public TravelRepository()
        {
            Configuration = GetConfiguration();
            _masterconnString = Configuration.GetConnectionString("MasterDBConnection");
            _txnconnString = Configuration.GetConnectionString("TransactionDBConnection");
            _WhitelabelconnString = Configuration.GetConnectionString("WhiteLabelDBconnection");
        }
        public IConfiguration GetConfiguration()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            return Configuration;
        }

        public DataSet GetBalance(GetBalanceRequest Request)
        {
            using (var connection = new SqlConnection(_WhitelabelconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_CheckBalance", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("RefID", Request.user_id));
                    cmd.Parameters.Add(new SqlParameter("Amount", Request.amount));
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);
                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public DataSet CreditDebitBalanceBalance(CreditDebitRequest Request,int type)
        {
            using (var connection = new SqlConnection(_WhitelabelconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_InsertWhitelabelTransaction", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("RefID", Request.particulars));
                    cmd.Parameters.Add(new SqlParameter("Amount", Request.offer_price));
                    cmd.Parameters.Add(new SqlParameter("OfferPrice", Request.user_id));
                    cmd.Parameters.Add(new SqlParameter("TransactionID", Request.merchant_id));
                    cmd.Parameters.Add(new SqlParameter("Remarks", Request.user_id));
                    cmd.Parameters.Add(new SqlParameter("UserRefID", Request.particulars));
                    cmd.Parameters.Add(new SqlParameter("IsdebitFlag", type));//1-debit 2-credit
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);
                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        public DataSet TransactionUpdate(TransactionUpdateRequest Request)
        {
            using (var connection = new SqlConnection(_WhitelabelconnString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("APT_UpdateWhitelabelTransaction", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("TransactionID", Request.transaction_id));
                    cmd.Parameters.Add(new SqlParameter("PNRNo", Request.pnr_no));
                    cmd.Parameters.Add(new SqlParameter("TransactionStatusRefID", Request.transaction_status));
                    cmd.Parameters.Add(new SqlParameter("UserRefID", Request.user_id));
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dataAdaper = new SqlDataAdapter(cmd);
                        DataSet dataSet = new DataSet();
                        dataAdaper.Fill(dataSet);
                        return dataSet;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
