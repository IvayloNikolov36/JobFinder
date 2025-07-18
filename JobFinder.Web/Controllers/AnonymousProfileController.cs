﻿using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.AnonymousProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    public class AnonymousProfileController : ApiController
    {
        private readonly IAnonymousProfileService anonymousProfileService;

        public AnonymousProfileController(
            IAnonymousProfileService anonymousProfileService)
        {
            this.anonymousProfileService = anonymousProfileService;
        }

        [HttpPost]
        [Route("create/{cvId:guid}")]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Create(
            [FromRoute] Guid cvId,
            [FromBody] AnonymousProfileCreateViewModel profile)
        {
            string userId = this.User.GetCurrentUserId();

            string id = await this.anonymousProfileService
                .Create(cvId.ToString(), userId, profile);

            return this.Ok(new { id });
        }

        [HttpDelete]
        [Route("delete/{id:guid}")]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateAnonymousProfileBelongsToUser))]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await this.anonymousProfileService.Delete(
                id.ToString(),
                this.User.GetCurrentUserId());

            return this.Ok();
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> GetMyAnonymousProfileData()
        {
            string userId = this.User.GetCurrentUserId();

            MyAnonymousProfileDataViewModel anonymousCvData = await this.anonymousProfileService
                .GetMyAnonymousProfileData(userId);

            return this.Ok(anonymousCvData);
        }

        [HttpGet]
        [Route("view/{id:guid}/{jobAdId:int}")]
        [Authorize(Roles = CompanyRole)]
        [ServiceFilter(typeof(ValidateJobAdBelongsToUser))]
        [ServiceFilter(typeof(ValidateCompanyCanViewAnonymousProfile))]
        public async Task<IActionResult> AnonymousProfilePreview([FromRoute] Guid id, [FromRoute] int jobAdId)
        {
            AnonymousProfileDataViewModel anonymousProfile = await this.anonymousProfileService
                .GetAnonymousProfile(
                    id.ToString(),
                    jobAdId,
                    this.User.GetCurrentUserId());

            return this.Ok(anonymousProfile);
        }
    }
}
