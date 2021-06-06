using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutoLot.Samples
{
  public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {
    public ApplicationDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
      var connectionString = @"Data Source=(localdb)\\mssqllocaldb;Integrated Security=true; Initial Catalog=AutoLot";

      optionsBuilder.UseSqlServer(connectionString);
      Console.WriteLine(connectionString);
      return new ApplicationDbContext(optionsBuilder.Options);
    }
  }
}
