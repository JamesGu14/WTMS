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
    
    public partial class saleshistory
    {
        public int id { get; set; }
        public Nullable<int> childId { get; set; }
        public Nullable<int> userId { get; set; }
    
        public virtual child child { get; set; }
        public virtual systemuser systemuser { get; set; }
    }
}
