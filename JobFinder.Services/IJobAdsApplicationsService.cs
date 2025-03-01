using JobFinder.Web.Models.JobAds;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobFinder.Services
{
    public interface IJobAdsApplicationsService
    {
        Task Create(JobAdApplicationInputModel jobAdApplication);

        Task<IEnumerable<JobAdApplicationViewModel>> GetAllMine(string userId);

        Task<IEnumerable<JobAdApplicationViewModel>> GetUserJobsAdApplications(string userId, int jobAdId);

        Task<IEnumerable<JobApplicationInfoViewModel>> GetCompanyJobAdApplications(string userId, int jobAdId);

        Task SetPreviewInfo(string cvId, int jobAdId);
    }
}
