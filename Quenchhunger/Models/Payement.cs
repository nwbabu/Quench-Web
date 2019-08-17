using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quenchhunger.Models
{
    public class Payement
    {
        public string MerchantId { get; set; }
        public string Tranx_id { get; set; }
        public string Tranx_amt { get; set; }
        public string Pay_amt { get; set; }
        public string Tranx_curr { get; set; }
        public string Cust_id { get; set; }
        public string Order_id { get; set; }
        public string Tranx_memo { get; set; }
        public string Tranx_noti_url { get; set; }
        public string HashValue { get; set; }
    }
}