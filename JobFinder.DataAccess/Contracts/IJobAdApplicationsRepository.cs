
namespace JobFinder.DataAccess.Contracts;

public interface IJobAdApplicationsRepository
{
    Task<bool> IsApplicationSent(string cvId, int jobAdId, string publisherId);
}
