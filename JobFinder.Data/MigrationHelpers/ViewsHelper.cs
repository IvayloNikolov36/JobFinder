namespace JobFinder.Data.MigrationHelpers
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public static class ViewsHelper
    {
        public static void CreateCompaniesSubscriptionsDataView(MigrationBuilder builder)
        {
            builder.Sql(@"CREATE VIEW [dbo].[CompanySubscriptionsData] AS
                            SELECT x.[CompanyId], x.[CompanyName], x.[Subscribers], 
                            STRING_AGG(j.Id, '; ') AS [JobIds] ,
                            STRING_AGG(j.Position, '; ') AS [JobPositions], 
                            STRING_AGG(j.Location, '; ') AS [JobLocations]
                            FROM
                            	(SELECT cs.[CompanyId], 
                            	c.[Name] As [CompanyName], 
                            	STRING_AGG(u.Email, '; ') AS [Subscribers]
                            	FROM CompanySubscriptions AS cs
                            	LEFT JOIN Companies AS c 
                            		ON cs.CompanyId = c.Id
                            	LEFT JOIN AspNetUsers AS u
                            		ON cs.UserId = u.Id
                            	GROUP BY cs.CompanyId, c.Name ) AS x
                            LEFT JOIN Companies AS c 
                            	ON x.CompanyId = c.Id
                            LEFT JOIN JobAds AS j 
                            	ON c.Id = j.PublisherId
                            WHERE DATEDIFF(DAY, j.CreatedOn, GETDATE()) <= 1
                            GROUP BY x.CompanyId, x.CompanyName, x.Subscribers");
        }

        public static void DropCompaniesSubscriptionsDataView(MigrationBuilder builder)
        {
            builder.Sql("DROP VIEW [dbo].[CompanySubscriptionsData]");
        }

        public static void CreateSubscriprionsByJobCategoryAndLocationView(MigrationBuilder builder)
        {
            builder.Sql(@"CREATE VIEW [dbo].[SubscriprionsByJobCategoryAndLocation] AS
                            SELECT jcs.[JobCategoryId], jc.[Type] AS [JobCategory], jcs.[Location], 
                            STRING_AGG(u.[Email], '; ') AS [Subscribers]
                            FROM JobCategorySubscriptions as jcs
                            JOIN AspNetUsers AS u
                            	ON jcs.[UserId] = u.[Id]
                            JOIN JobCategories AS jc
                            	ON jcs.JobCategoryId = jc.Id 
                            GROUP BY jcs.[JobCategoryId], jc.[Type], jcs.[Location]");
        }

        public static void DropSubscriprionsByJobCategoryAndLocationView(MigrationBuilder builder)
        {
            builder.Sql("DROP VIEW [dbo].[SubscriprionsByJobCategoryAndLocation]");
        }
    }
}
