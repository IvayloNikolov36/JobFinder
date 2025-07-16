using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddJobSeekerRoleToExistingUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO dbo.[AspNetUserRoles] ([UserId], [RoleId])
                SELECT u.[Id]
	                   ,(SELECT [Id] FROM dbo.[AspNetRoles] WHERE [Name] LIKE 'Job Seeker') 
                FROM dbo.[AspNetUsers] AS u
                LEFT JOIN Companies AS c
	                ON u.[Id] = c.[UserId]
                WHERE c.[Id] IS NULL
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE [dbo].[AspNetUserRoles]
                WHERE UserId IN (
	                SELECT u.[Id] 
	                FROM dbo.[AspNetUsers] AS u
	                LEFT JOIN dbo.[Companies] AS c
		                ON u.[Id] = c.[UserId]
	                WHERE c.[Id] IS NULL)
                AND RoleId = (SELECT [Id] FROM dbo.[AspNetRoles] WHERE [Name] LIKE 'Job Seeker')
            ");
        }
    }
}
