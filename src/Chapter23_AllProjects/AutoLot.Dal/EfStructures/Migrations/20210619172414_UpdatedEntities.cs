using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoLot.Dal.EfStructures.Migrations
{
  public partial class UpdatedEntities : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      MigrationHelpers.CreateSproc(migrationBuilder);
      MigrationHelpers.CreateCustomerOrderView(migrationBuilder);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      MigrationHelpers.DropProc(migrationBuilder);
      MigrationHelpers.DropCustomerOrderView(migrationBuilder);
    }
  }
}
