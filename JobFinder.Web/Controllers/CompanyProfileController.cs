using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.CompanyProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    public class CompanyProfileController : ApiController
    {
        private readonly ICompanyProfileService companyProfileService;

        public CompanyProfileController(ICompanyProfileService companyProfileService)
        {
            this.companyProfileService = companyProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProfileData()
        {
            string userId = this.User.GetCurrentUserId();

            CompanyProfileDataViewModel profileData = await this.companyProfileService.GetProfileData(userId);

            return this.Ok(profileData);
        }
    }
}
