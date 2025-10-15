using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.CloudImage;
using JobFinder.Web.Models.Common;
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
                .GetMyProfile(userId);

            return this.Ok(profileData);
        }

        [HttpPost]
        [Route("change-picture")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CloudImageViewModel))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> ChangeProfilePicture([FromForm] FileUploadViewModel model)
        {
            string userId = this.User.GetCurrentUserId();

            CloudImageViewModel imageData = await this.userProfileService
                .ChangeProfilePicture(userId, model.File);

            return this.Ok(imageData);
        }
    }
}
