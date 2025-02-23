using JobFinder.Web.Models.JobAds;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobFinder.Services
{
    public interface IJobAdsApplicationsService
    {
        Task Create(JobAdApplicationInputModel jobAdApplication);

        Task<IEnumerable<JobAdApplicationViewModel>> GetAllMine(string userId);

        Task<IEnumerable<JobAdApplicationViewModel>> GetJobAdApplications(int jobAdId, string userId);
    }
}
