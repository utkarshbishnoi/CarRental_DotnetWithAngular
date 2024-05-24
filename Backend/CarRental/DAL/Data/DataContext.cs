
using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RentalAgreementEntity> RentalAgreements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<RentalAgreementEntity>()
                .Property(oi => oi.TotalCost)
                .HasColumnType("decimal(18,2)");



            modelBuilder.Entity<CarEntity>()
                .Property(p => p.RentalPrice)
                .HasColumnType("decimal(18,2)");
        }

    }
}
