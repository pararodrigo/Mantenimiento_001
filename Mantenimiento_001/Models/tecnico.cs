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
    
    public partial class tecnico
    {
        public int tec_id { get; set; }
        public Nullable<int> inc_id { get; set; }
        public Nullable<int> users_id { get; set; }
    
        public virtual incidencia incidencia { get; set; }
        public virtual user user { get; set; }
    }
}