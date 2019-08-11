using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quenchhunger.Models;
namespace Quenchhunger.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        cartCheckOut checkout = new cartCheckOut();
        List<CartDetails> cartlist = null;
        public ActionResult cart()
        {
            if (Session["cartlist"] != null)
            {
                cartlist = (List<CartDetails>)Session["cartlist"];
                checkout.cartList = cartlist;
                checkout.cartTotal = cartlist.Sum(x => x.price);
            }
            return View(checkout);
        }
        public ActionResult RemoveItemFromCart()
        {
            int itemId = Convert.ToInt32(Request.QueryString["itemId"].ToString());
            if (Session["cartlist"] != null)
            {
                cartlist = (List<CartDetails>)Session["cartlist"];
                CartDetails cartItem = cartlist.Find(x => x.productId == itemId);
                cartlist.Remove(cartItem);
                checkout.cartList = cartlist;
                checkout.cartTotal = cartlist.Sum(x => x.price);
                Session["cart"] = Convert.ToInt32(Session["cart"]) - 1;
            }
            return View("cart", checkout);
        }
    }
}