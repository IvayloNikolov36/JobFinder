using Hangfire;
using JobFinder.Services;
using JobFinder.Services.Messages;
using JobFinder.Web.Models.Common;

namespace JobFinder.Web.Infrastructure.Extensions
{
    public static class RecurringJobManagerExtensions
    {
        public static void RegisterDeactiveJobAdvertisements(
            this IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            recurringJobManager.AddOrUpdate(
            "deactivating_JobAdvertisements_Published_MoreThan_30_DaysAgo",
                () => serviceProvider.GetService<IJobAdsService>().DeactivateAds(),
                WebConstants.DailyCronExpression);
        }

        public static void RegisterSendLatestCompanyJobAdvertisements(
            this IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            recurringJobManager.AddOrUpdate(
                $"sending_Latest_CompanyJobAds",
                () => serviceProvider.GetService<IDataSender>().SendLatestJobAdsForCompanySubscriptions(),
                WebConstants.DailyCronExpression);
        }

        public static void RegisterSendingJobAdvertisementsBySubscriptions(
            this IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            string[] cronExpressions =
            [
                WebConstants.DailyCronExpression,
                WebConstants.EverySundayCronExpression,
                WebConstants.FirstDayOfTheMonthCronExpression
            ];

            BasicViewModel[] recurringTypes = serviceProvider
                .GetService<INomenclatureService>()
                .GetRecurringTypesSync()
                .ToArray();

            int index = 0;

            foreach (string cronExpression in cronExpressions)
            {
                BasicViewModel recurringType = recurringTypes[index++];

                recurringJobManager.AddOrUpdate(
                    $"sending_{recurringType.Name}_JobAdsByCriterias",
                    () => serviceProvider.GetService<IDataSender>()
                        .SendLatestJobAdsForJobSubscriptions(recurringType.Id),
                    cronExpression);
            }
        }
    }
}
