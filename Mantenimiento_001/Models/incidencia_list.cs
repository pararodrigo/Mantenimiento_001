//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mantenimiento_001.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class incidencia_list
    {
        public int inc_id { get; set; }
        public string priority_name { get; set; }
        public string ty_name { get; set; }
        public string maq_name { get; set; }
        public Nullable<System.DateTime> s_date { get; set; }
        public Nullable<System.DateTime> f_date { get; set; }
        public string coment { get; set; }
        public string estado_name { get; set; }
        public string users_name { get; set; }
        public string users_fname { get; set; }
        public string usersf_name { get; set; }
        public string usersf_fname { get; set; }
    }
}
