﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Work.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BookStore1Entities2 : DbContext
    {
        public BookStore1Entities2()
            : base("name=BookStore1Entities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<author> authors { get; set; }
        public virtual DbSet<bill> bills { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<delivery> deliveries { get; set; }
        public virtual DbSet<FAQ> FAQs { get; set; }
        public virtual DbSet<favourite> favourites { get; set; }
        public virtual DbSet<feedback> feedbacks { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<payment> payments { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<subcategory> subcategories { get; set; }
        public virtual DbSet<supplier> suppliers { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
