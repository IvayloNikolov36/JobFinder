using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    public class UserProfileController : ApiController
    {
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

        [HttpGet]
        [Route("my-data")]
        public async Task<IActionResult> GetMyProfileData()
        {
            string userId = this.User.GetCurrentUserId();

            UserProfileDataViewModel profileData = await this.userProfileService.GetNyProfileData(userId);

            return this.Ok(profileData);
        }
    }
}
