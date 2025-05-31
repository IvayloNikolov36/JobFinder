
namespace JobFinder.DataAccess.Contracts;

public interface IJobAdRepository
{
    Task<string> GetPublisher(string userId, int jobAdId);
}
