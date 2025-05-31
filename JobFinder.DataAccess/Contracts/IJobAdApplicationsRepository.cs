
using JobFinder.Transfer.DTOs.JobAd;

namespace JobFinder.DataAccess.Contracts;

public interface IJobAdApplicationsRepository
{
    Task Create(JobAddApplicationInputDTO jobAdApplication);

    Task<IEnumerable<T>> GetJobAdApplications<T>(int jobAdId);

    Task<IEnumerable<JobAdApplicationDTO>> GetUserApplications(string userId);

    Task<bool> HasAlreadyApplied(string applicantId, int jobAdId);

    Task<bool> IsApplicationSent(string cvId, int jobAdId, string publisherId);

    Task<DateTime> SetPreviewed(string cvId, int jobAdId);
}
