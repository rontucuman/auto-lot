using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoLot.Dal.EfStructures
{
  public static class MigrationHelpers
  {
    public static void CreateSproc(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql($@"
        exec (N'
          CREATE PROCEDURE [dbo].[GetPetName]
            @p_CarId INT,
            @p_PetName NVARCHAR(50) OUTPUT
          AS
            SELECT @p_PetName = PetName from dbo.Inventory WHERE Id = @p_CarId  
        ')");
    }

    public static void DropProc(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("DROP PROCEDURE [dbo].[GetPetName]");
    }

    public static void CreateCustomerOrderView(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql($@"
        EXEC (N'
          CREATE VIEW [dbo].[CustomerOrderView]
          AS
            SELECT cs.FirstName, cs.LastName, 
              iv.Color, iv.PetName, iv.IsDrivable,
              mk.Name AS Make
            FROM dbo.[Order] AS od
            INNER JOIN dbo.Customer AS cs ON od.CustomerId = cs.Id
            INNER JOIN dbo.Inventory AS iv ON od.CarId = iv.Id
            INNER JOIN dbo.Make AS mk ON mk.Id = iv.MakeId
        ')");
    }

    public static void DropCustomerOrderView(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("EXEC (N' DROP VIEW [dbo].[CustomerOrderView]')");
    }
  }
}
