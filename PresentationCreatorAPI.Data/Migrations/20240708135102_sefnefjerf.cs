using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PresentationCreatorAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class sefnefjerf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 8, 13, 50, 58, 332, DateTimeKind.Utc).AddTicks(4982));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 7, 7, 4, 3, 38, 504, DateTimeKind.Utc).AddTicks(350));
        }
    }
}
