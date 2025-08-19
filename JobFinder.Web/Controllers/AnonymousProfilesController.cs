using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.AnonymousProfile;
using JobFinder.Web.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    [Route("api/anonymous-profiles")]
    public class AnonymousProfilesController : ApiController
    {
        private readonly IAnonymousProfilesService anonymousProfilesService;

        public AnonymousProfilesController(
            IAnonymousProfilesService anonymousProfileService)
        {
            this.anonymousProfilesService = anonymousProfileService;
        }

        [HttpGet]
        [Route("mine", Name = "MyAnonymousProfile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyAnonymousProfileDataViewModel))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> GetMyAnonymousProfileData()
        {
            string userId = this.User.GetCurrentUserId();

            MyAnonymousProfileDataViewModel anonymousCvData = await this.anonymousProfilesService
                .GetMyAnonymousProfileData(userId);

            return this.Ok(anonymousCvData);
        }

        [HttpPost]
        [Route("{cvId:guid}")]
        [Authorize(Roles = JobSeekerRole)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdentityViewModel<string>))]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Create(
            [FromRoute] Guid cvId,
            [FromBody] AnonymousProfileCreateViewModel profile)
        {
            string userId = this.User.GetCurrentUserId();

            IdentityViewModel<string> result = await this.anonymousProfilesService
                .Create(cvId.ToString(), userId, profile);

            return this.CreatedAtRoute("MyAnonymousProfile", result);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateAnonymousProfileBelongsToUser))]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await this.anonymousProfilesService.Delete(
                id.ToString(),
                this.User.GetCurrentUserId());

            return this.NoContent();
        }

        [HttpGet]
        [Route("{id:guid}/{jobAdId:int}")]
        [Authorize(Roles = CompanyRole)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnonymousProfileDataViewModel))]
        [ServiceFilter(typeof(ValidateJobAdBelongsToUser))]
        [ServiceFilter(typeof(ValidateCompanyCanViewAnonymousProfile))]
        public async Task<IActionResult> GetAnonymousProfileData(
            [FromRoute] Guid id,
            [FromRoute] int jobAdId)
        {
            AnonymousProfileDataViewModel anonymousProfile = await this.anonymousProfilesService
                .GetAnonymousProfile(
                    id.ToString(),
                    jobAdId,
                    this.User.GetCurrentUserId());

            return this.Ok(anonymousProfile);
        }
    }
}
