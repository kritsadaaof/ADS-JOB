﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JOB_ADS.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADS_Candidate> ADS_Candidate { get; set; }
        public virtual DbSet<ADS_Master_Department> ADS_Master_Department { get; set; }
        public virtual DbSet<ADS_Master_User> ADS_Master_User { get; set; }
        public virtual DbSet<ADS_PostJob> ADS_PostJob { get; set; }
        public virtual DbSet<ADS_Register> ADS_Register { get; set; }
        public virtual DbSet<SensitiveData> SensitiveData { get; set; }
        public virtual DbSet<ADS_Master_Email> ADS_Master_Email { get; set; }
    }
}
