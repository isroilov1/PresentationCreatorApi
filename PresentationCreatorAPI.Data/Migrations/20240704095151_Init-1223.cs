using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PresentationCreatorAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init1223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReferalId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ReferalId" },
                values: new object[] { new DateTime(2024, 7, 4, 9, 51, 48, 344, DateTimeKind.Utc).AddTicks(6841), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReferalId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ReferalId" },
                values: new object[] { new DateTime(2024, 6, 24, 10, 22, 25, 230, DateTimeKind.Utc).AddTicks(1498), 0 });
        }
    }
}
