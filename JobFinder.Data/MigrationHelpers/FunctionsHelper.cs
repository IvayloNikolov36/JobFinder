namespace JobFinder.Data.MigrationHelpers
{
	using Microsoft.EntityFrameworkCore.Migrations;
    using System;

    public static class FunctionsHelper
    {
		[Obsolete]
		public static void CreateGetLatesJobAdsForSubscribersFunction(MigrationBuilder builder)
		{
			builder.Sql(
                @"CREATE OR ALTER FUNCTION [dbo].[GetLatesJobAdsForSubscribers] 
				(@jobCategoryId INT, @locationId INT)
				RETURNS TABLE
				AS
				RETURN 
				SELECT
					c.[Id] AS [CompanyId]
					,c.[Name] AS [CompanyName]
					,c.[Logo] AS [CompanyLogoUrl]
					,STRING_AGG(ja.[Id], '; ') WITHIN GROUP(ORDER BY ja.[Id] DESC) AS [JobAdsIds]
					,STRING_AGG(ja.[Position], '; ') WITHIN GROUP(ORDER BY ja.[Id] DESC) AS [Positions]
				FROM JobAdvertisements AS ja
				JOIN Companies AS c 
					ON ja.PublisherId = c.Id
				WHERE ja.[JobCategoryId] = @jobCategoryId
				AND ja.[LocationId] = @locationId
				AND DATEDIFF(DAY, ja.[CreatedOn], GETUTCDATE()) <= 30
				GROUP BY c.[Id], c.[Name], c.[Logo]");
		}

		[Obsolete]
		public static void DropGetLatesJobAdsForSubscribersFunction(MigrationBuilder builder)
		{
			builder.Sql("DROP FUNCTION [dbo].[GetLatesJobAdsForSubscribers]");
		}

        public static void Create_UDF_GetLatesJobAdsForSubscribers(MigrationBuilder builder)
        {
            builder.Sql(
                @"CREATE OR ALTER FUNCTION [dbo].[udf_GetLatesJobAdsForSubscribers] 
				(@reccuringTypeId INT
				 ,@jobCategoryId INT
				 ,@jobEngagementId INT
				 ,@locationId INT
				 ,@searchTerm NVARCHAR
				 ,@intership BIT
				 ,@specifiedSalary BIT
				)
				RETURNS TABLE
				AS
				RETURN 
				SELECT
					c.[Id] AS [CompanyId]
					,c.[Name] AS [CompanyName]
					,c.[Logo] AS [CompanyLogoUrl]
					,STRING_AGG(ja.[Id], '; ') WITHIN GROUP(ORDER BY ja.[Id] DESC) AS [JobAdsIds]
					,STRING_AGG(ja.[Position], '; ') WITHIN GROUP(ORDER BY ja.[Id] DESC) AS [Positions]
				FROM JobAdvertisements AS ja
				JOIN Companies AS c 
					ON ja.PublisherId = c.Id
				WHERE ja.[JobCategoryId] = @jobCategoryId
					AND ja.[JobEngagementId] = @jobEngagementId
					AND ja.[Position] LIKE ('%' + @searchTerm + '%')
 					AND
					(
						@specifiedSalary = 0
						OR (@specifiedSalary = 1 AND (ja.MinSalary IS NOT NULL OR ja.MaxSalary IS NOT NULL))
					)										
					AND ja.[Intership] = @intership
					AND ja.[LocationId] = @locationId
					AND DATEDIFF(DAY, ja.[CreatedOn], GETUTCDATE()) <=
						CASE
							WHEN @jobCategoryId = 1 THEN 1
							WHEN @jobCategoryId = 2 THEN 7
							WHEN @jobCategoryId = 3 THEN 31
						END		 								
				GROUP BY c.[Id]
						 ,c.[Name]
						 ,c.[Logo]");
        }

        public static void Drop_UDF_GetLatesJobAdsForSubscribers(MigrationBuilder builder)
        {
            builder.Sql("DROP FUNCTION [dbo].[udf_GetLatesJobAdsForSubscribers] ");
        }

		public static void Create_UDF_GetLatestJobAdsForCompanySubscriptions(MigrationBuilder builder)
		{
			builder
				.Sql(@"CREATE OR ALTER FUNCTION [dbo].[udf_GetLatestJobAdsForCompanySubscriptions] (@recurringTypeId INT)
							RETURNS TABLE
							AS
							RETURN 
							SELECT x.[CompanyId]
							   ,x.[CompanyName]
							   ,x.[CompanyLogo]
							   ,x.[Subscribers]
							   ,STRING_AGG(j.Id, '; ') AS [JobIds]
							   ,STRING_AGG(j.Position, '; ') AS [JobPositions]
							   ,STRING_AGG(l.[Name], '; ') AS [JobLocations]
							FROM
								(SELECT cs.[CompanyId]
										,c.[Name] AS [CompanyName]
										,c.[Logo] AS [CompanyLogo]
										,STRING_AGG(u.Email, '; ') AS [Subscribers]
										FROM CompanySubscriptions AS cs
										LEFT JOIN Companies AS c 
											ON cs.CompanyId = c.Id
										LEFT JOIN AspNetUsers AS u
											ON cs.UserId = u.Id
										WHERE cs.RecuringTypeId = @recurringTypeId
										GROUP BY cs.[CompanyId], c.[Name], c.[Logo]
								) AS x
								LEFT JOIN Companies AS c 
									ON x.CompanyId = c.Id
								LEFT JOIN JobAdvertisements AS j 
									ON c.Id = j.PublisherId
								JOIN Cities AS l
									ON j.LocationId = l.Id
								WHERE DATEDIFF(DAY, j.CreatedOn, GETDATE()) <= 
									CASE
										WHEN @recurringTypeId = 1 THEN 1
										WHEN @recurringTypeId = 2 THEN 7
										WHEN @recurringTypeId = 3 THEN 31
									END
								GROUP BY x.[CompanyId]
										 ,x.[CompanyLogo]
										 ,x.[CompanyName]
										 ,x.[Subscribers]");
		}

		public static void Drop_UDF_GetLatestJobAdsForCompanySubscriptions(MigrationBuilder builder)
		{
			builder.Sql("DROP FUNCTION [dbo].[udf_GetLatestJobAdsForCompanySubscriptions]");
		}
    }
}
