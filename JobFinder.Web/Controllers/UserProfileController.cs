using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    [Route("api/user-profile")]
    public class UserProfileController : ApiController
    {
        private readonly IUserProfileService userProfileService;

        public UserProfileController(
            IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserProfileDataViewModel))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> GetMyProfileData()
        {
            string userId = this.User.GetCurrentUserId();

            UserProfileDataViewModel profileData = await this.userProfileService
                .GetNyProfileData(userId);

            return this.Ok(profileData);
        }
    }
}
