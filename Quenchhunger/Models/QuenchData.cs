using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using Quenchhunger.Models;

namespace Quenchhunger.Models
{
    public class QuenchData
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static object locker = new object();
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
                                  res_id = res.res_id,
                                  Restaurant_Name = res.Restaurant_Name,
                                  restaurant_Logo = res.restaurant_Logo,
                                  res_display_img = res.res_display_img,
                                  Category_Name = res.Category_Name
                              }).ToList();

                return result;
            }
        }
        public List<Product> getproduects(int resId)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                var result = (from pro in context.Uni_Product
                              join category in context.UNI_CATEGORY on pro.Prod_Cat_Id equals category.Category_id
                              where pro.Restaurant_Id == resId
                              select new Product
                              {
                                  id = pro.prod_id,
                                  Name = pro.Prod_name,
                                  Description = pro.Prod_Desc,
                                  Image = pro.Image1,
                                  Price = pro.Price.ToString(),
                                  Unit = pro.Prod_Unit,
                                  category= category.Category_Name
                              });
                return result.ToList();
            }

        }
        public List<Product> getTopProduects(int resId)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                return context.Uni_Product
                    .Where(c => c.Restaurant_Id == resId)
                    .Select(x => new Product()
                    {
                        id = x.prod_id,
                        Name = x.Prod_name,
                        Description = x.Prod_Desc,
                        Image = x.Image1,
                        Price = x.Price.ToString(),
                        Unit = x.Prod_Unit
                    }).Take(10).ToList();
            }
        }
        public Product getProduct(int productId)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                return context.Uni_Product
                    .Where(c => c.prod_id == productId)
                    .Select(x => new Product()
                    {
                        id = x.prod_id,
                        Name = x.Prod_name,
                        Description = x.Prod_Desc,
                        Image = x.Image1,
                        Price = x.Price.ToString(),
                        Unit = x.Prod_Unit
                    }).FirstOrDefault();
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
                                  IsChecked = false
                              }).ToList();
                return result;
            }
        }
        public bool SaveDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            try
            {
                using (s_foodEntities1 context = new s_foodEntities1())
                {
                    if (!context.UNI_DelveryAddress.Any(Address => Address.fullAddress == deliveryAddress.fullAddress))
                    {
                        context.UNI_DelveryAddress.Add(new UNI_DelveryAddress()
                        {
                            clientId = deliveryAddress.clientId,
                            firstName = deliveryAddress.firstName,
                            lastName = deliveryAddress.lastName,
                            country = deliveryAddress.country.ToString(),
                            fullAddress = deliveryAddress.fullAddress,
                            city = deliveryAddress.city,
                            state = deliveryAddress.state,
                            pincode = deliveryAddress.pincode,
                            emailAddress = deliveryAddress.emailAddress,
                            phone = deliveryAddress.phone
                        });
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DeliveryAddress getDeliveryAddressById(int id)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                return context.UNI_DelveryAddress
                      .Where(d => d.id == id)
                      .Select(x => new DeliveryAddress()
                      {
                          id = x.id,
                          clientId = x.clientId,
                          firstName = x.firstName,
                          lastName = x.lastName,
                          fullAddress = x.fullAddress,
                          state = x.state,
                          city = x.city,
                          emailAddress = x.emailAddress,
                          phone = x.phone,
                          pincode = x.pincode
                      }).FirstOrDefault();
            }
        }
        public List<DeliveryAddress> getDeliveryAddress(string clientId)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                return context.UNI_DelveryAddress
                       .Where(d => d.clientId == clientId)
                       .Select(x => new DeliveryAddress()
                       {
                           id = x.id,
                           clientId = x.clientId,
                           firstName = x.firstName,
                           lastName = x.lastName,
                           fullAddress = x.fullAddress,
                           state = x.state,
                           city = x.city,
                           emailAddress = x.emailAddress,
                           phone = x.phone,
                           pincode = x.pincode
                       }).ToList();
            }
        }
        public OrderDetails InsertOrder(OrderDetails order)
        {
            try
            {
                using (s_foodEntities1 context = new s_foodEntities1())
                {
                    context.Put_Order_Details(order.cust_id, order.restaurant_id,
                        order.productDetails, order.Delivery_Charges, order.Remark,
                        order.session_id, order.promo_code, order.Tot_Bill_Amt, order.discount, order.Recd_amt);
                    order = getOrderDetail(order);
                    return order;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public OrderDetails getOrderDetail(OrderDetails order)
        {
            try
            {
                using (s_foodEntities1 context = new s_foodEntities1())
                {
                    var resut = (from or in context.App_Manage_Order
                                 where or.cust_id == order.cust_id
                                      && or.restaurant_id == order.restaurant_id
                                 orderby or.order_date descending
                                 select new { or.order_id, or.OTP_no }).FirstOrDefault();
                    order.orderId = resut.order_id;
                    order.Otp = Convert.ToInt32(resut.OTP_no);
                    return order;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void UpdatePaymentDetails(string tranx_id)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                context.UpdateTrancationStatus(tranx_id);
            }
        }

        public bool InsertClientDetails(DeliveryAddress address)
        {
            try
            {
                using (s_foodEntities1 context = new s_foodEntities1())
                {
                    string cust_Address = address.fullAddress + "," + address.city + "," + address.state + "," + address.pincode;
                    if (!context.App_Manage_Client.Any(x => x.Cust_email == address.emailAddress && x.Cust_Mobile == address.phone))
                    {

                        context.PUT_Client_Detail(address.clientId, cust_Address, address.phone, address.emailAddress, "", "", "");
                        return true;
                    }
                    else
                    {
                        if (!context.App_Manage_Client.Any(x => x.cust_address == cust_Address
                        && x.Cust_email == address.emailAddress && x.Cust_Mobile == address.phone))
                        {
                            var Client = context.App_Manage_Client.Where(x => x.Cust_Mobile == address.phone &&
                            x.Cust_email == address.emailAddress)
                                .FirstOrDefault();
                            Client.cust_address = cust_Address;
                            context.SaveChanges();
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public int getCustomerId(string emailAddress, string Mobile)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {
                var result = context.App_Manage_Client.Where(x => x.Cust_email == emailAddress && x.Cust_Mobile == Mobile).
                             FirstOrDefault();
                return result.cust_code;
            }
        }
        public bool InsertPaymentDetails(Payement _payment, int custCode)
        {
            using (s_foodEntities1 context = new s_foodEntities1())
            {

                context.Put_Payment_Transanction(_payment.MerchantId, _payment.Tranx_id,
                    Convert.ToInt32(_payment.Order_id), custCode,
                    Convert.ToDecimal(_payment.Tranx_amt), _payment.Tranx_curr, _payment.Tranx_memo);
            }
            return true;
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
        public string Generate15UniqueDigits()
        {
            lock (locker)
            {
                Thread.Sleep(100);
                return DateTime.Now.ToString("yyyyMMddHHmmssf");
            }
        }

    }
}