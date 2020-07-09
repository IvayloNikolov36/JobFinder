namespace JobFinder.Services.Messages
{
    using System.Threading.Tasks;

    public interface IDataSender
    {
        Task SendSubscribersNewJobAds();
    }
}
