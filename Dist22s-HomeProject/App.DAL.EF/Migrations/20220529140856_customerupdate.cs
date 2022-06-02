using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    public partial class customerupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShippingInfoId",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ShippingInfoId",
                table: "Customers",
                column: "ShippingInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ShippingInfos_ShippingInfoId",
                table: "Customers",
                column: "ShippingInfoId",
                principalTable: "ShippingInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ShippingInfos_ShippingInfoId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ShippingInfoId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShippingInfoId",
                table: "Customers");
        }
    }
}
