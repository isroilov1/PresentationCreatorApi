using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagesPaths",
                table: "Pages",
                newName: "ImagesPath");

            migrationBuilder.AddColumn<int>(
                name: "PageType",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 19, 13, 51, 49, 344, DateTimeKind.Unspecified).AddTicks(2220));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageType",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "ImagesPath",
                table: "Pages",
                newName: "ImagesPaths");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 19, 10, 7, 37, 449, DateTimeKind.Unspecified).AddTicks(7487));
        }
    }
}
