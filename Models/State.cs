//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sample_CRUD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class State
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string Capital { get; set; }
        public int CountryFK { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual Country Country { get; set; }
    }
}
