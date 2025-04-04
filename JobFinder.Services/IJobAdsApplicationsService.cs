using JobFinder.Web.Models.AdApplication;

namespace JobFinder.Services
{
    public interface IJobAdsApplicationsService
    {
        Task Create(JobAdApplicationInputModel jobAdApplication);

        Task<IEnumerable<JobAdApplicationViewModel>> GetAllMine(string userId);

        Task<IEnumerable<JobAdApplicationViewModel>> GetUserJobsAdApplications(string userId, int jobAdId);

        Task<IEnumerable<JobApplicationInfoViewModel>> GetCompanyJobAdApplications(string userId, int jobAdId);

        Task<PreviewInfoViewModel> SetPreviewInfo(string cvId, int jobAdId);
    }
}
