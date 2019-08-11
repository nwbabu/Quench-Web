using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Quenchhunger.Models
{
    public class Restrurnat
    {
        public List<Category> foodCat { get; set; }
        public List<ResturantDetails> resDetails { get; set; }
        public List<Product> resProduct { get; set; }
    }
    public class ResturantDetails
    {
        public int res_id { get; set; }
        public string Restaurant_Name { get; set; }
        public string restaurant_Logo { get; set; }
        public string res_display_img { get; set; }
        public string Category_Name { get; set; }
    }
    public class Product
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        public string Unit { get; set; }
    }
    public class Category
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string image { get; set; }
        public bool IsChecked { get; set; }
    }
    public class CartDetails
    {
        public int productId { get; set; }
        public string prodductName { get; set; }
        public string productDes { get; set; }
        public int qty { get; set; }
        public int price { get; set; }

    }
    public class cartCheckOut
    {
       public  List<CartDetails> cartList { get; set; }
       public List<Product> resProducts { get; set; }
       public int cartTotal { get; set; }
       public List<DeliveryAddress> deliveryAddress { get; set; }

    }
    public class DeliveryAddress
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string country { get; set; }
        public string fullAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string emailAddress { get; set; }
        public string phone { get; set; }
    }
}