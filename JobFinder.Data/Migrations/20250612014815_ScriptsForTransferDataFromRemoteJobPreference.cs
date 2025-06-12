using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScriptsForTransferDataFromRemoteJobPreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AnonymousProfileAppearancesWorkplaceTypes]
                ([AnonymousProfileAppearanceId]
                 ,[WorkplaceTypeId]
                 ,[CreatedOn]
                )
                    SELECT
                        Id
                        ,1
                        ,GETUTCDATE()
                    FROM AnonymousProfileAppearances
                    WHERE RemoteJobPreferenceId = 2 OR RemoteJobPreferenceId = 3
                ");

            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AnonymousProfileAppearancesWorkplaceTypes]
                ([AnonymousProfileAppearanceId]
                 ,[WorkplaceTypeId]
                 ,[CreatedOn]
                )
                    SELECT
                        Id
                        ,2
                        ,GETUTCDATE()
                    FROM AnonymousProfileAppearances
                    WHERE RemoteJobPreferenceId = 1 OR RemoteJobPreferenceId = 2
                ");

            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AnonymousProfileAppearancesWorkplaceTypes]
                ([AnonymousProfileAppearanceId]
                 ,[WorkplaceTypeId]
                 ,[CreatedOn]
                )
                    SELECT
                        Id
                        ,3
                        ,GETUTCDATE()
                    FROM AnonymousProfileAppearances
                    WHERE RemoteJobPreferenceId = 2 OR RemoteJobPreferenceId = 3
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
