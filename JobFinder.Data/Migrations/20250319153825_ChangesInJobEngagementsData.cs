using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInJobEngagementsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE JobAdvertisements
                                   SET Intership = 1
                                   WHERE JobEngagementId = 5");

            migrationBuilder.Sql(@"UPDATE JobAdvertisements
                                   SET JobEngagementId = 4 --Temporary
                                   WHERE JobEngagementId = 5");

            migrationBuilder.Sql(@"UPDATE JobAdvertisements
                                   SET JobEngagementId = 5
                                   WHERE JobEngagementId = 6");

            migrationBuilder.Sql(@"UPDATE JobAdvertisements
                                   SET JobEngagementId = 6
                                   WHERE JobEngagementId = 7");

            migrationBuilder.DeleteData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Full time");

            migrationBuilder.UpdateData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Part time");

            migrationBuilder.UpdateData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Suitable for students");

            migrationBuilder.UpdateData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Suitable for candidates with no expirience");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "FullTime");

            migrationBuilder.UpdateData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "PartTime");

            migrationBuilder.InsertData(
                table: "JobEngagements",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[] { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suitable for candidates with no expirience" });


            migrationBuilder.Sql(@"UPDATE JobAdvertisements
                                SET JobEngagementId = 7
                                WHERE JobEngagementId = 6");

            migrationBuilder.Sql(@"UPDATE JobAdvertisements
                                SET JobEngagementId = 6
                                WHERE JobEngagementId = 5");

            migrationBuilder.Sql(@"UPDATE JobAdvertisements
                                SET JobEngagementId = 5
                                WHERE JobEngagementId = 4
                                    AND Intership = 1");

            migrationBuilder.UpdateData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Intership");

            migrationBuilder.UpdateData(
                table: "JobEngagements",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Suitable for students");

        }
    }
}
