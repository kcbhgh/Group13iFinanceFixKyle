﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Group13_iFINANCEDBEntities1 : DbContext
    {
        public Group13_iFINANCEDBEntities1()
            : base("name=Group13_iFINANCEDBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccountCategory> AccountCategory { get; set; }
        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<FinanceTransaction> FinanceTransaction { get; set; }
        public virtual DbSet<GroupTable> GroupTable { get; set; }
        public virtual DbSet<iFINANCEUser> iFINANCEUser { get; set; }
        public virtual DbSet<MasterAccount> MasterAccount { get; set; }
        public virtual DbSet<NonAdminUser> NonAdminUser { get; set; }
        public virtual DbSet<TransactionLine> TransactionLine { get; set; }
        public virtual DbSet<UserPassword> UserPassword { get; set; }
    }
}
