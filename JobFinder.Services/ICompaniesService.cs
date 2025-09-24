using JobFinder.Web.Models.Company;

namespace JobFinder.Services
{
    public interface ICompaniesService
    {
        Task<int> GetCompanyId(string userId);

        Task<CompanyDetailsUserViewModel> Details(int companyId, string currentUserId);

        Task<CompanyJobAdsListingViewModel> AllActiveAds(int companyId);

        Task<IEnumerable<CompanyListingViewModel>> GetAll(string userId);
    }
}
