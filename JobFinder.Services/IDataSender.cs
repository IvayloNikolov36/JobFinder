namespace JobFinder.Services.Messages
{
    using System.Threading.Tasks;

    public interface IDataSender
    {
        Task SendLatestJobAdsForCompanySubscriptions();

        Task SendLatestJobAdsForJobSubscriptions();
    }
}
