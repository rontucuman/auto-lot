using AutoLot.Dal.EfStructures.Configurations;
using Microsoft.EntityFrameworkCore;
using AutoLot.Model.Entities;

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

        public virtual DbSet<SeriLogEntry>? LogEntries { get; set; }
        public virtual DbSet<CreditRisk>? CreditRisks { get; set; }
        public virtual DbSet<Customer>? Customers { get; set; }
        public virtual DbSet<Car>? Inventories { get; set; }
        public virtual DbSet<Make>? Makes { get; set; }
        public virtual DbSet<Order>? Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            new SeriLogEntryEntityConfiguration().Configure(modelBuilder.Entity<SeriLogEntry>());

            new CreditRiskEntityConfiguration().Configure(modelBuilder.Entity<CreditRisk>());

            new CustomerEntityConfiguration().Configure(modelBuilder.Entity<Customer>());

            new CarEntityConfiguration().Configure(modelBuilder.Entity<Car>());

            new MakeEntityConfiguration().Configure(modelBuilder.Entity<Make>());

            new OrderEntityConfiguration().Configure(modelBuilder.Entity<Order>());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
