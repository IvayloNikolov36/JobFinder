using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 19, 59, 56, 685, DateTimeKind.Utc).AddTicks(2498));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 19, 59, 56, 685, DateTimeKind.Utc).AddTicks(2500));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 19, 59, 56, 685, DateTimeKind.Utc).AddTicks(2501));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 19, 59, 56, 685, DateTimeKind.Utc).AddTicks(2502));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 19, 59, 56, 685, DateTimeKind.Utc).AddTicks(2503));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 19, 59, 56, 685, DateTimeKind.Utc).AddTicks(2504));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 19, 59, 56, 685, DateTimeKind.Utc).AddTicks(2504));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 19, 59, 56, 685, DateTimeKind.Utc).AddTicks(2505));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 19, 59, 56, 685, DateTimeKind.Utc).AddTicks(2506));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 15, 50, 41, 862, DateTimeKind.Utc).AddTicks(6478));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 15, 50, 41, 862, DateTimeKind.Utc).AddTicks(6480));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 15, 50, 41, 862, DateTimeKind.Utc).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 15, 50, 41, 862, DateTimeKind.Utc).AddTicks(6482));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 15, 50, 41, 862, DateTimeKind.Utc).AddTicks(6483));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 15, 50, 41, 862, DateTimeKind.Utc).AddTicks(6484));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 15, 50, 41, 862, DateTimeKind.Utc).AddTicks(6484));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 15, 50, 41, 862, DateTimeKind.Utc).AddTicks(6485));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedOn",
                value: new DateTime(2025, 3, 8, 15, 50, 41, 862, DateTimeKind.Utc).AddTicks(6486));
        }
    }
}
