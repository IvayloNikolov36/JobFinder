namespace JobFinder.Data.MigrationHelpers
{
	using Microsoft.EntityFrameworkCore.Migrations;

	public static class FunctionsHelper
    {
		public static void CreateGetLatesJobAdsForSubscribersFunction(MigrationBuilder builder)
		{
			builder.Sql(
				@"CREATE FUNCTION [dbo].[GetLatesJobAdsForSubscribers] 
				  (@jobCategoryId INT, @location NVARCHAR(250))
				  RETURNS TABLE
				  AS
				  RETURN 
					SELECT 
				  	c.[Id], c.[Name], c.[Logo],
				  	STRING_AGG(ja.[Id], '; ') WITHIN GROUP(ORDER BY ja.[Id] DESC) AS [JobAdsIds], 
				  	STRING_AGG(ja.[Position], '; ') WITHIN GROUP(ORDER BY ja.[Id] DESC) AS [Positions]
				  	FROM JobAds AS ja
				  	JOIN Companies AS c 
				  		ON ja.PublisherId = c.Id
				  	WHERE ja.[JobCategoryId] = @jobCategoryId
				  	AND ja.[Location] LIKE @location
				  	AND DATEDIFF(DAY, ja.[CreatedOn], GETDATE()) <= 1
				  	GROUP BY c.[Id], c.[Name], c.[Logo]");
		}

		public static void DropGetLatesJobAdsForSubscribersFunction(MigrationBuilder builder)
		{
			builder.Sql("DROP FUNCTION [dbo].[GetLatesJobAdsForSubscribers]");
		}
	}
}
