using System;
using AutoLot.Dal.EfStructures.Configurations;
using AutoLot.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;
using AutoLot.Model.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

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
      base.SavingChanges += (sender, args) =>
      {
        Console.WriteLine($"Saving Changes for {((ApplicationDbContext)sender!).Database.GetConnectionString()}");
      };

      base.SavedChanges += (sender, args) =>
      {
        Console.WriteLine(
          $"Saved {args.EntitiesSavedCount} changes for {((ApplicationDbContext) sender!).Database.GetConnectionString()}");
      };

      base.SaveChangesFailed += (sender, args) =>
      {
        Console.WriteLine($"An exception occurred! {args.Exception.Message} entities");
      };

      ChangeTracker.Tracked += ChangeTrackerOnTracked;
      ChangeTracker.StateChanged += ChangeTrackerOnStateChanged;
    }

    private void ChangeTrackerOnStateChanged(object? sender, EntityStateChangedEventArgs e)
    {
      if (e.Entry.Entity is not Car c)
      {
        return;
      }

      string action = string.Empty;

      Console.WriteLine($"Car {c.PetName} was {e.OldState} before the state changed to {e.NewState}");

      switch (e.NewState)
      {
        case EntityState.Detached:
          break;
        case EntityState.Unchanged:
          action = e.OldState switch
          {
            EntityState.Modified => "Edited",
            EntityState.Added => "Added",
            _ => action
          };
          Console.WriteLine($"The object was {action}");
          break;
        case EntityState.Deleted:
          break;
        case EntityState.Modified:
          break;
        case EntityState.Added:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void ChangeTrackerOnTracked(object? sender, EntityTrackedEventArgs e)
    {
      throw new NotImplementedException();
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

    public override int SaveChanges()
    {
      try
      {
        return base.SaveChanges();
      }
      catch (DbUpdateConcurrencyException e)
      {
        // A concurrency error occurred
        // Should log and handle intelligently
        throw new CustomConcurrencyException("A concurrency error happened.", e);
      }
      catch (RetryLimitExceededException e)
      {
        // DbResiliency retry limit exceeded
        // Should log and handle intelligently
        throw new CustomRetryLimitExceededException("There is a problem with Sql Server.", e);
      }
      catch (DbUpdateException e)
      {
        // Should log and handle intelligently
        throw new CustomDbUpdateException("An error occurred updating the database.", e);
      }
      catch (Exception e)
      {
        // Should log and handle intelligently
        throw new CustomException("An error occurred updating the database.", e);
      }
    }
  }
}
