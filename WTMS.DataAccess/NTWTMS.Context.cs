﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ntwtmsEntities : DbContext
    {
        public ntwtmsEntities()
            : base("name=ntwtmsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<child> children { get; set; }
        public DbSet<childparentrel> childparentrels { get; set; }
        public DbSet<contacthistory> contacthistories { get; set; }
        public DbSet<parent> parents { get; set; }
        public DbSet<saleshistory> saleshistories { get; set; }
        public DbSet<systemuser> systemusers { get; set; }
        public DbSet<userstatu> userstatus { get; set; }
        public DbSet<wtmsrole> wtmsroles { get; set; }
    }
}
