﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoftwareProject.Models.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MeDiagEntities2 : DbContext
    {
        public MeDiagEntities2()
            : base("name=MeDiagEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Community> Community { get; set; }
        public virtual DbSet<DAppDate> DAppDate { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Discharge> Discharge { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Hospital> Hospital { get; set; }
        public virtual DbSet<Illness> Illness { get; set; }
        public virtual DbSet<Op_Table> Op_Table { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
    }
}