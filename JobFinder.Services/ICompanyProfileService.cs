using JobFinder.Web.Models.CloudImage;
using JobFinder.Web.Models.CompanyProfile;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services;

public interface ICompanyProfileService
{
    Task<CompanyProfileDataViewModel> GetProfileData(string userId);

    Task<CloudImageViewModel> ChangeLogo(string userId, IFormFile file);
}
