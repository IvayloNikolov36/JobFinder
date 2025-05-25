using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.AnonymousProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    public class AnonymousProfileController : ApiController
    {
        private readonly IAnonymousProfileService anonymousProfileService;

        public AnonymousProfileController(IAnonymousProfileService anonymousProfileService)
        {
            this.anonymousProfileService = anonymousProfileService;
        }

        [HttpPost]
        [Route("activate")]
        public async Task<IActionResult> Activate(AnonymousProfileCreateViewModel profile)
        {
            string currentUserId = this.User.GetCurrentUserId();

            await this.anonymousProfileService.Create(currentUserId, profile);

            return this.Ok();
        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetMyAnonymousProfileData()
        {
            string userId = this.User.GetCurrentUserId();

            AnonymousProfileCvDataViewModel anonymousCvData = await this.anonymousProfileService
                .GetAnonymousProfileData(userId);

            return this.Ok(anonymousCvData);
        }
    }
}
