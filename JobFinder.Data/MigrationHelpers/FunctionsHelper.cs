namespace JobFinder.Data.MigrationHelpers
{
	using Microsoft.EntityFrameworkCore.Migrations;

	public static class FunctionsHelper
    {
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

		public static void DropGetLatesJobAdsForSubscribersFunction(MigrationBuilder builder)
		{
			builder.Sql("DROP FUNCTION [dbo].[GetLatesJobAdsForSubscribers]");
		}
	}
}
