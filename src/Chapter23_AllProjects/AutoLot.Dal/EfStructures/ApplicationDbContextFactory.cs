using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutoLot.Dal.EfStructures
{
  public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {
    public ApplicationDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
      string connectionString = @"Data Source=(localdb)\\mssqllocaldb;Integrated Security=true; Initial Catalog=AutoLot";

      optionsBuilder.UseSqlServer(connectionString);

      return new ApplicationDbContext(optionsBuilder.Options);
    }
  }
}
