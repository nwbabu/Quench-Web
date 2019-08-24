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
            Session["Action"] = "Restaurants";
            string param = "";
            try
            {
                param = Request.QueryString["Restaurants"].ToString();
            }
            catch
            {

            }
            TempData["res_id"] = 0;
            restaurant.resDetails = quenchData.getResturantDetails(param);
            TempData["resDetails"]  = restaurant.resDetails;
            TempData.Keep();
            ViewBag.Message = "Your Restaurant page. ";
            return View(restaurant);
        }
        public ActionResult RestaurantsMenu()
        {
            Session["Action"] = "RestaurantsMenu";
            try
            {
                int res_id = Convert.ToInt32(Request.QueryString["res_id"].ToString());
                Session["res_id"] = res_id;
                TempData["res_id"] = res_id;
                TempData.Keep();
                restaurant.resProduct = quenchData.getproduects(res_id);
                TempData["ProduectDetails"] = restaurant.resProduct;
                TempData.Keep();
                return View(restaurant);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public PartialViewResult category()
        {
            int res_id = 0;
            List<Category> foodcat;
            try
            {
                res_id = Convert.ToInt32(TempData["res_id"].ToString());
            }
            catch
            {

            }
            if (res_id != 0)
            {
                foodcat = quenchData.getFoodCategory(res_id);
            }
            else
            {
                foodcat = quenchData.getFoodCategory();
            }
            if (TempData["catName"]!=null)
            {
                string catName = TempData["catName"].ToString();
                for (int i = 0; i < foodcat.Count; i++)
                {
                    if (IsExists(catName, foodcat[i].Name))
                    {
                        foodcat[i].IsChecked = true;
                    }
                }
            }
            restaurant.foodCat = foodcat;
            return PartialView("CategoryPartail", restaurant);
        }
        public PartialViewResult banner()
        {
            return PartialView("Banner");
        }
        [HttpPost]
        public ActionResult applyCategoryFilter(FormCollection frm)
        {
            List<Product> proList;
            List<ResturantDetails> resDetails;
            if (Session["Action"] != null)
            {
                string catName = frm["categorys"];
                TempData["catName"] = catName;
                string amtFilter = frm["filterAmount"];
                string controller = Session["Action"].ToString();
                if (controller == "RestaurantsMenu")
                {
                    proList = (List<Product>)TempData["ProduectDetails"];
                    TempData.Keep();
                    restaurant.resProduct = proList.Where(pro => 
                           IsExists(catName,pro.category)
                        ).ToList();
                    return View("RestaurantsMenu", restaurant);
                }
                else
                {
                    resDetails= (List<ResturantDetails>)TempData["resDetails"];
                    restaurant.resDetails = resDetails.Where(r => IsCatExits(r.Category_Name, catName)).ToList();
                    TempData.Keep();
                    return View("Restaurants", restaurant);
                }
            }
            return View ("Index");
        }
        private bool IsExists(string list,string value)
        {
            var categoryList = list.Split(',').ToList();
            return categoryList.Any(x => x == value);
        }
        private bool IsCatExits(string catitem, string catFilter)
        {
            var categoryList = catitem.Split(',').ToList();
            var catFilterList = catFilter.Split(',').ToList();
            foreach (var item in catFilterList)
            {
                return categoryList.Any(x => x.Contains(item));
            }
            return false;
        }
    }
}