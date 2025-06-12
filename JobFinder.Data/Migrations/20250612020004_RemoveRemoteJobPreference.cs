using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRemoteJobPreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousProfileAppearances_RemoteJobPreferences_RemoteJobPreferenceId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropTable(
                name: "RemoteJobPreferences");

            migrationBuilder.DropIndex(
                name: "IX_AnonymousProfileAppearances_RemoteJobPreferenceId",
                table: "AnonymousProfileAppearances");

            migrationBuilder.DropColumn(
                name: "RemoteJobPreferenceId",
                table: "AnonymousProfileAppearances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemoteJobPreferenceId",
                table: "AnonymousProfileAppearances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RemoteJobPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteJobPreferences", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RemoteJobPreferences",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "only remote job offers" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "interested in remote job offers" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "not interested in remote job offers" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousProfileAppearances_RemoteJobPreferenceId",
                table: "AnonymousProfileAppearances",
                column: "RemoteJobPreferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousProfileAppearances_RemoteJobPreferences_RemoteJobPreferenceId",
                table: "AnonymousProfileAppearances",
                column: "RemoteJobPreferenceId",
                principalTable: "RemoteJobPreferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
