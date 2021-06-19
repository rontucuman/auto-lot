using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoLot.Dal.EfStructures.Migrations
{
    public partial class EntitiesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Make",
                newName: "Make",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customer",
                newSchema: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "CreditRisk",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                computedColumnSql: "[LastName]+ ', ' + [FirstName]");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "dbo",
                table: "Customer",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true,
                computedColumnSql: "[LastName] + ', ' + [FirstName]");

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "DateTime", nullable: true, defaultValueSql: "GetDate()"),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "Xml", nullable: true),
                    LogEvent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceContext = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Order",
                table: "Order",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Order",
                table: "Order");

            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "CreditRisk");

            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "dbo",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Make",
                schema: "dbo",
                newName: "Make");

            migrationBuilder.RenameTable(
                name: "Customer",
                schema: "dbo",
                newName: "Customer");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Order",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
