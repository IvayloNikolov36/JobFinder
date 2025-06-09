using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobFinder.Data.Migrations
{
    /// <inheritdoc />
    public partial class ScriptsToCreateAnonymousProfileEntitiesForEveryAnonymousProfileAppearance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AnonymousProfiles] ([Id], [UserId], [CurriculumVitaeId], [CreatedOn])
                SELECT
                    CONVERT(nvarchar(450), NEWID())
	                ,(SELECT UserId FROM dbo.[CurriculumVitaes] AS cv WHERE cv.[Id] = apa.[CurriculumVitaeId])
	                ,apa.[CurriculumVitaeId]
                    ,GETUTCDATE()
                FROM [dbo].[AnonymousProfileAppearances] AS apa
            ");

            migrationBuilder.Sql(@"
                UPDATE dbo.[AnonymousProfileAppearances]
                SET AnonymousProfileId = (SELECT Id FROM dbo.[AnonymousProfiles] AS ap
                    WHERE ap.[CurriculumVitaeId] = CurriculumVitaeId)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
