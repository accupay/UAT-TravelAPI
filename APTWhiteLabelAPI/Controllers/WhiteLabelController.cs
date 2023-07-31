using APTWhiteLabelAPI.BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using APTWhiteLabelAPI.BusinessLayer;

namespace APTWhiteLabelAPI.Controllers
{
    [Route("api/Travel")]
    [ApiController]
    public class WhiteLabelController : Controller
    {
        TravelAPI ta=new TravelAPI();
       
        [HttpPost]
        [Route("CheckBalance")]
        public IActionResult? CheckBalance(GetBalanceRequest gbrq)
        {
            GetBalanceResponse objresponse = new GetBalanceResponse();
            try
            {
                objresponse = ta.GetBalance(gbrq);
                if (objresponse != null)
                {
                        return Ok(objresponse);
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(objresponse);
            }
        }

        [HttpPost]
        [Route("CreditAPI")]
        public IActionResult? Credit(CreditDebitRequest gbrq)
        {
            CreditDebitResponse objresponse = new CreditDebitResponse();
            try
            {
                objresponse = ta.Credit(gbrq);
                if (objresponse != null)
                {
                    return Ok(objresponse);
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(objresponse);
            }
        }

        [HttpPost]
        [Route("DebitAPI")]
        public IActionResult? Debit(CreditDebitRequest gbrq)
        {
            CreditDebitResponse objresponse = new CreditDebitResponse();
            try
            {
                objresponse = ta.Debit(gbrq);
                if (objresponse != null)
                {
                    return Ok(objresponse);
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(objresponse);
            }
        }

        [HttpPost]
        [Route("TransactionStatus")]
        public IActionResult? TransactionStatus(TransactionUpdateRequest gbrq)
        {
            TransactionUpdateResponse objresponse = new TransactionUpdateResponse();
            try
            {
                objresponse = ta.TransactionUpdate(gbrq);
                if (objresponse != null)
                {
                    return Ok(objresponse);
                }
                else
                {
                    return UnprocessableEntity(objresponse);
                }
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(objresponse);
            }
        }

        //[HttpGet]
        //[Route("Checktest")]
        //public int test()
        //{
        //    return 0;
        //}

    }
}
