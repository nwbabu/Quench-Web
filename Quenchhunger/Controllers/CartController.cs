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
        QuenchData quenchData = new QuenchData();
        List<CartDetails> cartlist = null;
        CartDetails cartItem;
        public ActionResult cart()
        {
            int res_id = Convert.ToInt32(Session["res_id"].ToString());
            checkout.resProducts = quenchData.getTopProduects(res_id);
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
            int res_id = Convert.ToInt32(Session["res_id"].ToString());
            checkout.resProducts = quenchData.getTopProduects(res_id);
            if (Session["cartlist"] != null)
            {
                cartlist = (List<CartDetails>)Session["cartlist"];
                CartDetails cartItem = cartlist.Find(x => x.productId == itemId);
                cartlist.Remove(cartItem);
                checkout.cartList = cartlist;
                checkout.cartTotal = cartlist.Sum(x => x.price);
                Session["cartlist"] = cartlist;
                Session["cart"] = Convert.ToInt32(Session["cart"]) - 1;
            }
            return View("cart", checkout);
        }
        public ActionResult AddItem()
        {
            int proDuctId = Convert.ToInt32(Request.QueryString["itemId"]);
            Product product = quenchData.getProduct(proDuctId);
            int res_id = Convert.ToInt32(Session["res_id"].ToString());
            checkout.resProducts = quenchData.getTopProduects(res_id);
            if (Session["cartlist"] != null)
            {
                cartlist = (List<CartDetails>)Session["cartlist"];
                cartItem = cartlist.Where(x => x.productId == proDuctId).FirstOrDefault();
                if(cartItem!=null)
                {
                    cartItem.qty += 1;
                    cartItem.price += cartItem.price;
                }
                else
                {
                    cartItem = new CartDetails()
                    {
                        productId = product.id,
                        prodductName = product.Name,
                        productDes = product.Description,
                        price = Convert.ToInt32(double.Parse(product.Price)) ,
                        qty = 1
                    };
                    cartlist.Add(cartItem);
                }
                if (Session["cart"] == null)
                {
                    Session["cart"] = 1;
                }
                else
                {
                    Session["cart"] = Convert.ToInt32(Session["cart"]) + 1;
                }
                checkout.cartList = cartlist;
                checkout.cartTotal = cartlist.Sum(x => x.price);
            }
            return View("cart", checkout);
        }
    }
}