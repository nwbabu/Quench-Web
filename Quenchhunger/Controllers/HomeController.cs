using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quenchhunger.Models;

namespace Quenchhunger.Controllers
{
    public class HomeController : Controller
    {
        QuenchData quenchData = new QuenchData();
        Restrurnat restaurant = new Restrurnat();

        public ActionResult Index()
        {
            Session["cart"] = 0;
            restaurant.resDetails = quenchData.getTopResturantDetails();
            return View(restaurant);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Restaurants()
        {
            string param = "";
            try {
                param = Request.QueryString["Restaurants"].ToString();
            }
            catch
            {

            }
            TempData["res_id"] = 0;
            TempData.Keep();
            restaurant.resDetails = quenchData.getResturantDetails(param);
            restaurant.foodCat = quenchData.getFoodCategory();
            ViewBag.Message = "Your Restaurant page. ";
            return View(restaurant);
        }
        public ActionResult RestaurantsMenu()
        {
            try {
                int res_id = Convert.ToInt32( Request.QueryString["res_id"].ToString());
                Session["res_id"] = res_id;
                TempData["res_id"] = res_id;
                TempData.Keep();
                restaurant.resProduct = quenchData.getproduects(res_id);
                return View(restaurant);
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        public PartialViewResult category()
        {
            int res_id = 0;
            try {
                 res_id = Convert.ToInt32(TempData["res_id"].ToString());
            }
            catch
            {

            }
            if (res_id != 0)
            {
                restaurant.foodCat = quenchData.getFoodCategory(res_id);
            }
            else
            {
                restaurant.foodCat = quenchData.getFoodCategory();
            }

            return PartialView("CategoryPartail", restaurant);
        }
        public PartialViewResult banner()
        {
            return PartialView("Banner");
        }
        [HttpPost]
        public ActionResult applyCategoryFilter(FormCollection frm)
        {
            string LoadView = Request.UrlReferrer.ToString();
            string catIDs = frm["categorys"];
            string amtFilter = frm["filterAmount"];

            return View();
        }
    }
}