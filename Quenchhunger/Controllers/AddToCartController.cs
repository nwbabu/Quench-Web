using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quenchhunger.Models;
using Microsoft.AspNet.Identity;
namespace Quenchhunger.Controllers
{
    public class AddToCartController : Controller
    {
        // GET: AddToCart
        public ActionResult Add()
        {
            QuenchData quenchData = new QuenchData();
            List<CartDetails> cartlist=null;
            if (Session["cartlist"] == null)
            {
                cartlist = new List<CartDetails>();
            }
            else
            {
                cartlist = (List < CartDetails >) Session["cartlist"];
            }
            CartDetails cart;
            Produect product;
            int productId = Convert.ToInt32(Request.QueryString["productId"].ToString());
            int res_id = Convert.ToInt32(Session["res_id"].ToString());
            cart = cartlist.Where(x => x.productId == productId).FirstOrDefault();
            if(cart!=null)
            {
                cart.qty += 1;
                cart.price += cart.price;
            }
            else
            {
                product = quenchData.getProduct(productId);
                cart = new CartDetails() {
                    productId = product.id,
                    prodductName = product.Name,
                    productDes = product.Description,
                    price = Convert.ToInt32(double.Parse(product.Price)) * 100,
                    qty = 1
                };
                cartlist.Add(cart);
            }
            Session["cartlist"] = cartlist;
            string userId = User.Identity.GetUserName();
            if (Session["cart"] == null)
            {
                Session["cart"] = 1;
            }
            else
            {
                Session["cart"]=  Convert.ToInt32(Session["cart"]) + 1;
            }
            return Redirect("~/Home/RestaurantsMenu?res_id=" + res_id);
        }
    }
}