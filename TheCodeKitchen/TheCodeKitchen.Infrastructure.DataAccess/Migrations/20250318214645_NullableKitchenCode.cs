using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheCodeKitchen.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NullabmeKitchenCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Kitchens_Code",
                table: "Kitchens");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Kitchens",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Started",
                table: "Games",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kitchens_Code",
                table: "Kitchens",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Kitchens_Code",
                table: "Kitchens");

            migrationBuilder.DropColumn(
                name: "Started",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Kitchens",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kitchens_Code",
                table: "Kitchens",
                column: "Code",
                unique: true);
        }
    }
}
