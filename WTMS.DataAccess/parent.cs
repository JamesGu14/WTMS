//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WTMS.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class parent
    {
        public parent()
        {
            this.childparentrels = new HashSet<childparentrel>();
            this.contacthistories = new HashSet<contacthistory>();
        }
    
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string relationToChild { get; set; }
        public string mobile { get; set; }
        public string password { get; set; }
        public System.DateTime createdAt { get; set; }
        public bool isActive { get; set; }
        public Nullable<System.DateTime> deactivateAt { get; set; }
        public int statusId { get; set; }
        public string comment { get; set; }
    
        public virtual ICollection<childparentrel> childparentrels { get; set; }
        public virtual ICollection<contacthistory> contacthistories { get; set; }
        public virtual userstatu userstatu { get; set; }
    }
}
