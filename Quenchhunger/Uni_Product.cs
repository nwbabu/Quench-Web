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
    
    public partial class Uni_Product
    {
        public int prod_id { get; set; }
        public string Prod_name { get; set; }
        public Nullable<int> Prod_Cat_Id { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Prod_Unit { get; set; }
        public string Prod_Desc { get; set; }
        public string Image1 { get; set; }
        public Nullable<int> Restaurant_Id { get; set; }
        public Nullable<int> Author_Id { get; set; }
        public Nullable<int> Update_Author_Id { get; set; }
        public Nullable<System.DateTime> Entry_date { get; set; }
        public Nullable<System.DateTime> update_entry_date { get; set; }
        public string Prod_Status { get; set; }
        public Nullable<int> prepration_time { get; set; }
        public string prepration_uom { get; set; }
    }
}
