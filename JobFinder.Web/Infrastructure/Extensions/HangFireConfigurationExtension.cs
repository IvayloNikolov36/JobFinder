namespace JobFinder.Web.Infrastructure.Extensions
{
    using Hangfire;
    using Hangfire.SqlServer;
    using System;

    public static class HangFireConfigurationExtension
    {
        public static void Set(this IGlobalConfiguration configuration, string connectionString)
        {
            configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString,
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    });
        }
    }
}
