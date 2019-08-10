using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using Quenchhunger.Models;

namespace Quenchhunger.Models
{
    public class QuenchData
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public List<ResturantDetails> getResturantDetails(string searchResturnats)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                string[] address = searchResturnats.Split(',');
                string UniArea = string.Empty, unicity = string.Empty, uniState = string.Empty;
                try
                {
                    if (address.Length >= 3)
                    {
                        UniArea = address[0].ToString().Trim();
                        unicity = address[1].ToString().Trim();
                        uniState = address[2].ToString().Trim();
                    }
                    if (address.Length == 2)
                    {
                        UniArea = address[0].ToString().Trim();
                        unicity = address[1].ToString().Trim();
                    }
                    else
                    {
                        uniState = "";
                    }
                    if (address.Length == 1)
                    {
                        UniArea = address[0].ToString().Trim();

                    }
                    else
                    {
                        unicity = "";
                        uniState = "";
                    }

                }
                catch
                {
                    UniArea = "";
                    unicity = "";
                    uniState = "";
                }

                var result = (from res in context.uni_Restaurant
                              join area in context.uni_area on res.Area_Id equals area.Area_id
                              join city in context.UNI_CITY on res.city_id equals city.city_id
                              join state in context.uni_state on res.State_id equals state.state_id
                              where area.Area_name.Contains(UniArea) &&
                              city.City_Name.Contains(unicity) &&
                              state.State_name.Contains(uniState)
                              select new ResturantDetails
                              {
                                  res_id=res.res_id,
                                  Restaurant_Name = res.Restaurant_Name,
                                  restaurant_Logo = res.restaurant_Logo,
                                  res_display_img = res.res_display_img,
                                  Category_Name = res.Category_Name
                              }).ToList();

                return result;
            }
        }
        public List<Produect> getproduects(int resId)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                return context.Uni_Product
                    .Where(c=>c.Restaurant_Id== resId)
                    .Select(x => new Produect()
                    {
                        id = x.prod_id,
                        Name = x.Prod_name,
                        Description = x.Prod_Desc,
                        Image = x.Image1,
                        Price = x.Price.ToString(),
                        Unit = x.Prod_Unit
                    }).ToList();
            }
        }
        public List<Category> getFoodCategory(int resId)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                var result = (from prod in context.Uni_Product
                              join cat in context.UNI_CATEGORY on prod.Prod_Cat_Id equals cat.Category_id
                              where prod.Restaurant_Id == resId
                              select new Category
                              {
                                  id = cat.Category_id,
                                  Name = cat.Category_Name,
                                  image = cat.Image1,
                                  IsChecked = false
                              }).ToList();
                return result;
            }
        }
        public List<Category> getFoodCategory()
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                var result = (from cat in context.UNI_CATEGORY
                             
                              select new Category
                              {
                                  id = cat.Category_id,
                                  Name = cat.Category_Name,
                                  image = cat.Image1,
                                  IsChecked=false
                              }).ToList();
                return result;
            }
        }
        public List<ResturantDetails> getTopResturantDetails()
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                return context.uni_Restaurant
                     .Select(x => new ResturantDetails()
                     {
                         res_id = x.res_id,
                         Restaurant_Name = x.Restaurant_Name,
                         restaurant_Logo = x.restaurant_Logo,
                         res_display_img = x.res_display_img
                     }).Take(6).ToList();

            }
        }
        private DataTable getDataTableFromCommand(SqlCommand command)
        {
            using (SqlDataReader dr = command.ExecuteReader())
            {
                var tb = new DataTable();
                tb.Load(dr);
                return tb;
            }
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        //public List<ResturantDetails> getRestaurant(string category)
        //{
        //    using (s_foodEntities1 context = new s_foodEntities1())
        //    {
        //        return context.uni_Restaurant
        //            .Where(c => c.Category_Name == category)
        //            .Select(x => new ResturantDetails()
        //            {
        //                res_id = x.res_id,
        //                Restaurant_Name = x.Restaurant_Name,
        //                restaurant_Logo = x.restaurant_Logo,
        //                res_display_img = x.res_display_img
        //            }).Take(6).ToList();
        //    }
        //}
    }
}