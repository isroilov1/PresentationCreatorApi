using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PresentationCreatorAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Isegmhderh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 10, 10, 42, 30, 323, DateTimeKind.Utc).AddTicks(9257));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 8, 13, 50, 58, 332, DateTimeKind.Utc).AddTicks(4982));
        }
    }
}
