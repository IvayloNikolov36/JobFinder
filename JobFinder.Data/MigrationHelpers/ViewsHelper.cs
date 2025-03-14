﻿namespace JobFinder.Data.MigrationHelpers
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;

    public static class ViewsHelper
    {
        [Obsolete]
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

        [Obsolete]
        public static void DropCompaniesSubscriptionsDataView(MigrationBuilder builder)
        {
            builder.Sql("DROP VIEW [dbo].[CompanySubscriptionsData]");
        }

        [Obsolete]
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

        [Obsolete]
        public static void DropSubscriprionsByJobCategoryAndLocationView(MigrationBuilder builder)
        {
            builder.Sql("DROP VIEW [dbo].[SubscriprionsByJobCategoryAndLocation]");
        }

        public static void Create_View_JobSubscriptions(MigrationBuilder builder)
        {
            builder.Sql(@"CREATE OR ALTER VIEW [dbo].[view_JobSubscriptions]
                            AS
	                            SELECT	js.ReccuringTypeId
			                            ,rt.[Name] AS [ReccuringType]
			                            ,js.[JobCategoryId]
			                            ,jc.[Name] AS [JobCategory]
			                            ,js.[JobEngagementId]
			                            ,je.[Name] AS [JobEngagement]
			                            ,js.[LocationId]
			                            ,ci.[Name] AS [Location]
			                            ,js.[SearchTerm]
			                            ,js.[Intership]
			                            ,js.[SpecifiedSalary]
			                            ,STRING_AGG(u.[Email], '; ') AS [SubscribersEmails]
	                            FROM JobsSubscriptions AS js
	                            JOIN ReccuringTypes AS rt
		                            ON js.ReccuringTypeId = rt.[Id]
	                            LEFT JOIN JobCategories AS jc
		                            ON js.JobCategoryId = jc.[Id]
	                            LEFT JOIN JobEngagements AS je
		                            ON js.JobEngagementId = je.[Id]
	                            LEFT JOIN Cities AS ci
		                            ON js.LocationId = ci.[Id]
	                            JOIN AspNetUsers AS u
		                            ON js.[UserId] = u.[Id]
	                            GROUP BY js.[ReccuringTypeId]
			                            ,rt.[Name]
			                            ,js.[JobCategoryId]
			                            ,jc.[Name]
			                            ,js.[JobEngagementId]
			                            ,je.[Name]
			                            ,js.[LocationId]
			                            ,ci.[Name]
			                            ,js.[SearchTerm]
			                            ,js.[Intership]
			                            ,js.[SpecifiedSalary]");
        }

        public static void Drop_View_JobSubscriptions(MigrationBuilder builder)
        {
            builder.Sql("DROP VIEW [dbo].[view_JobSubscriptions]");
        }
    }
}
