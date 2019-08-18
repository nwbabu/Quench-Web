﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quenchhunger.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace Quenchhunger.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        cartCheckOut checkout = new cartCheckOut();
        QuenchData quenchData = new QuenchData();
        List<CartDetails> cartlist = null;
        Payement _payment = new Payement();
        OrderDetails orderDt = null;
        Encript encript = new Encript();
        public ActionResult getParameters()
        {
            if (Session["cartlist"] != null && Session["order"] != null)
            {
                cartlist = (List<CartDetails>)Session["cartlist"];
                orderDt = (OrderDetails)Session["order"];
                _payment.Cust_id = User.Identity.GetUserName();
                _payment.MerchantId = "824";
                _payment.Tranx_memo = "Mobow";
                _payment.Tranx_amt = (cartlist.Sum(x => x.price)).ToString();
                _payment.Pay_amt = ((cartlist.Sum(x => x.price)) * 100).ToString();
                _payment.Tranx_id = quenchData.Generate15UniqueDigits();
                _payment.Tranx_curr = "566";
                _payment.Order_id = orderDt.orderId.ToString();
                _payment.Tranx_noti_url = "http://quenchhunger.com/Quenchhunger-Web/Payment/getResponse";
                string getHash = _payment.MerchantId.Trim() + _payment.Tranx_id.Trim() + _payment.Pay_amt.Trim() +
                    _payment.Tranx_curr + _payment.Cust_id.Trim() + _payment.Tranx_noti_url.Trim() + encript.hashkey;
                getHash = encript.GenerateSHA512String(getHash.Trim());
                _payment.HashValue = getHash;

                int custId = orderDt.cust_id;
                quenchData.InsertPaymentDetails(_payment, custId);
              
            }
            return View(_payment);
        }
        public ActionResult getResponse()
        {
            TranssctionResponse _response = new TranssctionResponse();
            string tranx_id = Request.Form["gtpay_tranx_id"].ToString();
            string tranx_amt_small_denom = Request.Form["gtpay_tranx_amt_small_denom"].ToString();
            string tranx_status_code = Request.Form["gtpay_tranx_status_code"].ToString();
            string gtpay_tranx_curr = Request.Form["gtpay_tranx_curr"].ToString();
            string tranx_amt = Request.Form["gtpay_tranx_amt"].ToString();
            string full_verification_hash = Request.Form["gtpay_full_verification_hash"].ToString();
            string you_hash = tranx_id + tranx_amt_small_denom + tranx_status_code + gtpay_tranx_curr + encript.hashkey;
            you_hash = encript.GenerateSHA512String(you_hash);
            _response.tranx_id = tranx_id;
            _response.tranx_amt_small_denom = tranx_amt_small_denom;
            _response.tranx_status_code = tranx_status_code;
            _response.tranx_amt = tranx_amt;
            _response.gtpay_tranx_curr = gtpay_tranx_curr;
            _response.userName = User.Identity.GetUserName();
            if (you_hash.Equals(full_verification_hash))
            {
                string hash_req = encript.MarchantId + tranx_id + encript.hashkey;
                hash_req = encript.GenerateSHA512String(hash_req);
                string parameters = "mertid=" + encript.MarchantId + "&amount=" + tranx_amt_small_denom + "&tranxid=" + tranx_id + "&hash=" + hash_req;
                string reuestUrl = "https://ibank.gtbank.com/GTPayService/gettransactionstatus.json?" + parameters;
                string response = _payment.callurl(reuestUrl);
                var res = JsonConvert.DeserializeObject<dynamic>(response);
                if (res["ResponseCode"] == "00")
                {
                    quenchData.UpdatePaymentDetails(tranx_id);
                    Session["cartlist"] = null;
                    Session["cart"] = null;
                }
            }
            return View(_response);
        }

    }
   
}