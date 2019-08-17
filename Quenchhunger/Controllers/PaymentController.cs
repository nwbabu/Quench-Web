using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quenchhunger.Models;
using Microsoft.AspNet.Identity;

namespace Quenchhunger.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        cartCheckOut checkout = new cartCheckOut();
        QuenchData quenchData = new QuenchData();
        List<CartDetails> cartlist = null;
        Payement _payment = new Payement();
        Encript encript = new Encript();
        public ActionResult getParameters()
        {
            if (Session["cartlist"] != null)
            {
                cartlist = (List<CartDetails>)Session["cartlist"];
                _payment.Cust_id = User.Identity.GetUserName();
                _payment.MerchantId = "824";
                _payment.Tranx_memo = "Mobow";
                _payment.Tranx_amt = (cartlist.Sum(x => x.price)).ToString();
                _payment.Pay_amt= ((cartlist.Sum(x => x.price))*100).ToString();
                _payment.Tranx_id = quenchData.Generate15UniqueDigits();
                _payment.Tranx_curr = "566";
                _payment.Order_id = "545555";
                _payment.Tranx_noti_url = "http://localhost:9106/Payment/getResponse";
                string getHash = _payment.MerchantId.Trim() + _payment.Tranx_id.Trim() + _payment.Pay_amt.Trim() +
                    _payment.Tranx_curr + _payment.Cust_id.Trim() + _payment.Tranx_noti_url.Trim() + encript.hashkey;
                getHash = encript.GenerateSHA512String(getHash.Trim());
                _payment.HashValue = getHash;
                if (Session["cust_code"] != null)
                {
                    int custId = Convert.ToInt32(Session["cust_code"].ToString());
                    quenchData.InsertPaymentDetails(_payment, custId);
                }

            }
            return View(_payment);
        }
        public ActionResult getResponse()
        {
            return View();
        }
    }
}