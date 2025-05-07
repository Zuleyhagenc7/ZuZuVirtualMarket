using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZuZuVirtual.Data.Migrations
{
    /// <inheritdoc />
    public partial class SiparişlerTablosu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 4, 25, 18, 48, 18, 534, DateTimeKind.Local).AddTicks(4419), new Guid("501bd9d7-1979-4589-ad32-301ded41f114") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 4, 25, 18, 48, 18, 538, DateTimeKind.Local).AddTicks(8289));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 4, 25, 18, 48, 18, 538, DateTimeKind.Local).AddTicks(9905));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 4, 25, 17, 53, 24, 987, DateTimeKind.Local).AddTicks(7984), new Guid("4c13399b-6c67-407b-a31f-8ce8ca67f083") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 4, 25, 17, 53, 24, 990, DateTimeKind.Local).AddTicks(8953));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 4, 25, 17, 53, 24, 991, DateTimeKind.Local).AddTicks(88));
        }
    }
}
