//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Group13iFinanceFix.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Administrator
    {
        public string ID { get; set; }
        public Nullable<System.DateTime> dateHired { get; set; }
        public Nullable<System.DateTime> dateFinished { get; set; }
    
        public virtual iFINANCEUser iFINANCEUser { get; set; }
    }
}
