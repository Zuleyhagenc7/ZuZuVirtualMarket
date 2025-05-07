using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZuZuVirtual.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewsConfigurationonDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "News",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(750)",
                oldMaxLength: 750,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 4, 6, 19, 29, 31, 29, DateTimeKind.Local).AddTicks(2938), new Guid("79c7250e-0293-43b4-9b73-1ba04432fc80") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 4, 6, 19, 29, 31, 32, DateTimeKind.Local).AddTicks(7448));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 4, 6, 19, 29, 31, 32, DateTimeKind.Local).AddTicks(8412));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "News",
                type: "nvarchar(750)",
                maxLength: 750,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 3, 15, 22, 3, 56, 256, DateTimeKind.Local).AddTicks(7606), new Guid("f48f9baa-c3b9-474f-b7bd-b2975dddd2c1") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 3, 15, 22, 3, 56, 260, DateTimeKind.Local).AddTicks(5338));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 3, 15, 22, 3, 56, 260, DateTimeKind.Local).AddTicks(6513));
        }
    }
}
