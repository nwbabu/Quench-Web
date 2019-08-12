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
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string loginId = User.Identity.GetUserName();
                cartCheckOut checkout = new cartCheckOut();
                List<CartDetails> cartlist = null;
                if (Session["cartlist"] != null)
                {
                    cartlist = (List<CartDetails>)Session["cartlist"];
                    checkout.cartList = cartlist;
                    checkout.cartTotal = cartlist.Sum(x => x.price);
                }
                return View(checkout);
            }
            else
            {
                string loginId = User.Identity.GetUserName();
                return RedirectToAction("Login", "Account");
            }
        }
    }
}