namespace JobFinder.Services;

public interface IDataSender
{
    Task SendLatestJobAdsForCompanySubscriptions();

    Task SendLatestJobAdsForJobSubscriptions(int recurringTypeId);
}
