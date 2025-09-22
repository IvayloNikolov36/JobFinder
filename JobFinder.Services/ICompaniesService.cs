using JobFinder.Web.Models.Company;
using JobFinder.Web.Models.JobAds;

namespace JobFinder.Services
{
    public interface ICompaniesService
    {
        Task<int> GetCompanyId(string userId);

        Task<CompanyDetailsUserViewModel> Details(int companyId, string currentUserId);

        Task<CompanyJobAdsListingViewModel> AllActiveAds(int companyId);
    }
}
