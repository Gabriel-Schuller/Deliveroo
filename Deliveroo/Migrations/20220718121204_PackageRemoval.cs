using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deliveroo.Migrations
{
    public partial class PackageRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressID",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "AproxCost",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBaggages",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalWeight",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AproxCost",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NumberOfBaggages",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalWeight",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageID);
                    table.ForeignKey(
                        name: "FK_Packages_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressID",
                table: "Users",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_OrderID",
                table: "Packages",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressID",
                table: "Users",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
