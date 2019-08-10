using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quenchhunger.Controllers
{
    public class AddToCartController : Controller
    {
        // GET: AddToCart
        public ActionResult Add()
        {
            int res_id = Convert.ToInt32(Session["res_id"].ToString());
            if (Session["cart"] == null)
            {
                Session["cart"] = 1;
            }
            else
            {
                Session["cart"]=  Convert.ToInt32(Session["cart"]) + 1;
            }
            //return RedirectToAction("Restaurants", "Home");
            return Redirect("~/Home/RestaurantsMenu?res_id=" + res_id);
        }
    }
}