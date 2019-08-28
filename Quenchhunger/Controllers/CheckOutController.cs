using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quenchhunger.Models;
using Microsoft.AspNet.Identity;
namespace Quenchhunger.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        QuenchData quenchData = new QuenchData();
        List<CartDetails> cartlist = null;
        cartCheckOut checkout = new cartCheckOut();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string loginId = User.Identity.GetUserName();
                
               
                if (Session["cartlist"] != null)
                {
                    cartlist = (List<CartDetails>)Session["cartlist"];
                    checkout.cartList = cartlist;
                    checkout.deliveryAddress = quenchData.getDeliveryAddress(loginId);
                    checkout.DeliveryCharage = 0;
                    checkout.cartTotal = cartlist.Sum(x => x.disCountPrice) ;
                    int offerAmount = (cartlist.Sum(x => x.price)) - checkout.cartTotal;
                    checkout.Offer = offerAmount;
                    checkout.NetPayAmount = checkout.cartTotal + checkout.DeliveryCharage;
                    Session["checkout"] = checkout;
                }
                return View(checkout);
            }
            else
            {
                string loginId = User.Identity.GetUserName();
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult AddDeliveryAddress(DeliveryAddress da)  {

            return View();
        }

        [HttpPost]
        public ActionResult Save(DeliveryAddress address)
        {
            address.clientId= User.Identity.GetUserName();
            quenchData.SaveDeliveryAddress(address);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SaveOrder(FormCollection frm)
        {
            OrderDetails orderDt = new OrderDetails();
            int  addressId = Convert.ToInt32( frm["address"]);
            DeliveryAddress delAddress = quenchData.getDeliveryAddressById(addressId);
            quenchData.InsertClientDetails(delAddress);
            int cust_code = quenchData.getCustomerId(delAddress.emailAddress, delAddress.phone);
            if (Session["checkout"] != null)
            {
                checkout = (cartCheckOut)Session["checkout"];
                cartlist = checkout.cartList;
                string productDetails = "(";
               
                foreach (var item in cartlist)
                {
                    productDetails+= item.productId + "," + item.qty + "," + item.price + ";";
                }
                productDetails += ")";
                int res_id = Convert.ToInt32(Session["res_id"].ToString());
                orderDt.cust_id = cust_code;
                orderDt.restaurant_id = res_id;
                orderDt.Tot_Bill_Amt=checkout.NetPayAmount;
                orderDt.productDetails = productDetails;
                orderDt.session_id = Session.SessionID;
                orderDt.Delivery_Charges = checkout.DeliveryCharage;
                orderDt.discount = checkout.Offer;
                orderDt.promo_code = "";
                orderDt.Recd_amt = 0;
                orderDt.Remark = "";
                orderDt = quenchData.InsertOrder(orderDt);
                Session["order"] = orderDt;

                //Random rng = new Random();
                ////Fetching OTP Characters
                //string OtpCharacters = OTPGenerate.OTPCharacters();
                ////Createing More Secure OTP Password by Using MD5 algorithm
                //string OTPPassword = OTPGenerate.OTPGenerator(OtpCharacters, rng.Next(10).ToString());
                
            }
            return RedirectToAction("getParameters", "Payment");
        }

    }
}