﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC5Library.Models.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbLibraryEntities : DbContext
    {
        public DbLibraryEntities()
            : base("name=DbLibraryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TBLAuthor> TBLAuthor { get; set; }
        public virtual DbSet<TBLBook> TBLBook { get; set; }
        public virtual DbSet<TBLCategory> TBLCategory { get; set; }
        public virtual DbSet<TBLCriminal> TBLCriminal { get; set; }
        public virtual DbSet<TBLEmployee> TBLEmployee { get; set; }
        public virtual DbSet<TBLMovement> TBLMovement { get; set; }
        public virtual DbSet<TBLSafe> TBLSafe { get; set; }
        public virtual DbSet<TBLUser> TBLUser { get; set; }
    }
}