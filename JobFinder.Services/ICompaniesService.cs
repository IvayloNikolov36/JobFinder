using JobFinder.Web.Models.Company;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services
{
    public interface ICompaniesService
    {
        Task<int> GetCompanyId(string userId);

        Task<CompanyDetailsUserViewModel> Details(int companyId, string currentUserId);

        Task<CompanyJobAdsListingViewModel> AllActiveAds(int companyId);

        Task<IEnumerable<CompanyListingViewModel>> GetAll(string userId);

        Task SetLogo(int id, string userId, IFormFile logo);
    }
}
