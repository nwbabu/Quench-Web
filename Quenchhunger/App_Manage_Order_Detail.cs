//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quenchhunger
{
    using System;
    using System.Collections.Generic;
    
    public partial class App_Manage_Order_Detail
    {
        public int srno { get; set; }
        public Nullable<int> order_id { get; set; }
        public Nullable<int> prod_id { get; set; }
        public Nullable<int> qty { get; set; }
        public Nullable<decimal> bill_amt { get; set; }
        public Nullable<decimal> tax_per { get; set; }
        public Nullable<decimal> tax_amt { get; set; }
        public Nullable<decimal> Total_bill_amt { get; set; }
        public string fin_id { get; set; }
        public string qty_mes { get; set; }
        public string Order_delivery_status { get; set; }
    }
}
