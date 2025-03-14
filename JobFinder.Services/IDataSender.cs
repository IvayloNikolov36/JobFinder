namespace JobFinder.Services.Messages
{
    using System.Threading.Tasks;

    public interface IDataSender
    {
        Task SendLatestJobAdsForCompanySubscriptions(int recurringTypeId);

        Task SendLatestJobAdsForJobSubscriptions(int recurringTypeId);
    }
}
