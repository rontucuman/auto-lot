using System;
using AutoLot.Dal.EfStructures.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoLot.Model.Entities;

#nullable disable

namespace AutoLot.Dal.EfStructures
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Car> Inventories { get; set; }
        public virtual DbSet<Make> Makes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CreditRisk>(entity =>
            {
                entity.ToTable("CreditRisk");

                entity.HasIndex(e => e.CustomerId, "IX_FK_Customer_CreditRisk");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.CreditRisks)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_CreditRisk");
            });

            new CustomerEntityConfiguration().Configure(modelBuilder.Entity<Customer>());

            new CarEntityConfiguration().Configure(modelBuilder.Entity<Car>());

            new MakeEntityConfiguration().Configure(modelBuilder.Entity<Make>());

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasIndex(e => new { e.CustomerId, e.CarId }, "IX_FK_Customer_Order");

                entity.HasIndex(e => e.CarId, "IX_FK_Inventory_Order");

                entity.Property(e => e.TimeStamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.CarNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Order");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Order");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
