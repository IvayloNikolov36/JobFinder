namespace JobFinder.Data.MigrationHelpers
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public static class ViewsHelper
    {
        public static void CreateCompaniesSubscriptionsDataView(MigrationBuilder builder)
        {
            builder.Sql(@"CREATE VIEW [dbo].[CompanySubscriptionsData] AS
                            SELECT x.[CompanyId], x.[CompanyName], x.[CompanyLogo], x.[Subscribers], 
                            STRING_AGG(j.Id, '; ') AS [JobIds] ,
                            STRING_AGG(j.Position, '; ') AS [JobPositions], 
                            STRING_AGG(j.Location, '; ') AS [JobLocations]
                            FROM
                            	(SELECT cs.[CompanyId], 
                            	c.[Name] AS [CompanyName],
								c.[Logo] AS [CompanyLogo], 
                            	STRING_AGG(u.Email, '; ') AS [Subscribers]
                            	FROM CompanySubscriptions AS cs
                            	LEFT JOIN Companies AS c 
                            		ON cs.CompanyId = c.Id
                            	LEFT JOIN AspNetUsers AS u
                            		ON cs.UserId = u.Id
                            	GROUP BY cs.[CompanyId], c.[Name], c.[Logo]) AS x
                            LEFT JOIN Companies AS c 
                            	ON x.CompanyId = c.Id
                            LEFT JOIN JobAdvertisements AS j 
                            	ON c.Id = j.PublisherId
                            WHERE DATEDIFF(DAY, j.CreatedOn, GETDATE()) <= 1
                            GROUP BY x.[CompanyId], x.[CompanyLogo], x.[CompanyName], x.[Subscribers]");
        }

        public static void DropCompaniesSubscriptionsDataView(MigrationBuilder builder)
        {
            builder.Sql("DROP VIEW [dbo].[CompanySubscriptionsData]");
        }

        public static void CreateSubscriprionsByJobCategoryAndLocationView(MigrationBuilder builder)
        {
            builder.Sql(@"CREATE OR ALTER VIEW [dbo].[SubscriprionsByJobCategoryAndLocation]
                AS
	                SELECT	js.[JobCategoryId]
			                ,jc.[Name] AS [JobCategory]
			                ,js.[LocationId]
			                ,ci.[Name] AS [Location]
			                ,STRING_AGG(u.[Email], '; ') AS [SubscribersEmails]
	                FROM JobsSubscriptions as js
	                JOIN Cities AS ci
		                ON js.LocationId = ci.Id
	                JOIN AspNetUsers AS u
		                ON js.[UserId] = u.[Id]
	                JOIN JobCategories AS jc
		                ON js.JobCategoryId = jc.Id 
	                GROUP BY js.[JobCategoryId], jc.[Name], js.[LocationId], ci.[Name]");
        }

        public static void DropSubscriprionsByJobCategoryAndLocationView(MigrationBuilder builder)
        {
            builder.Sql("DROP VIEW [dbo].[SubscriprionsByJobCategoryAndLocation]");
        }
    }
}
