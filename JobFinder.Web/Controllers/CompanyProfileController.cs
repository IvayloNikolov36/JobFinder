using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.CompanyProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    [Route("api/company-profile")]
    public class CompanyProfileController : ApiController
    {
        private readonly ICompanyProfileService companyProfileService;

        public CompanyProfileController(ICompanyProfileService companyProfileService)
        {
            this.companyProfileService = companyProfileService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyProfileDataViewModel))]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> GetMyProfileData()
        {
            string userId = this.User.GetCurrentUserId();

            CompanyProfileDataViewModel profileData = await this.companyProfileService
                .GetProfileData(userId);

            return this.Ok(profileData);
        }
    }
}
