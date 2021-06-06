using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLot.Samples.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoLot.Samples
{
  public partial class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    static void SampleSaveChanges()
    {
      var context = new ApplicationDbContextFactory().CreateDbContext(null);
      context.SaveChanges();
    }
  }
}
