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
    
    public partial class App_Manage_Payment_Transanction
    {
        public int Payment_Transaction_Id { get; set; }
        public string Merchant_id { get; set; }
        public string Transaction_id { get; set; }
        public Nullable<int> Order_Id { get; set; }
        public Nullable<int> Cust_Id { get; set; }
        public string Bank_Transaction_id { get; set; }
        public Nullable<decimal> Transaction_amt { get; set; }
        public string Transaction_Currency { get; set; }
        public string Delivert_Transanction_Status { get; set; }
        public string tranx_memo { get; set; }
        public Nullable<System.DateTime> Transaction_Date { get; set; }
        public Nullable<System.DateTime> Payment_Date { get; set; }
        public string Transaction_time { get; set; }
    }
}