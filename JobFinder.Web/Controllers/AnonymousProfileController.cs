﻿using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Infrastructure.Filters;
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
        [Route("{cvId:guid}/activate")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Activate(
            [FromRoute] Guid cvId,
            [FromBody] AnonymousProfileCreateViewModel profile)
        {
            string userId = this.User.GetCurrentUserId();

            await this.anonymousProfileService.Activate(cvId.ToString(), userId, profile);

            return this.Ok();
        }

        [HttpGet]
        [Route("{cvId:guid}/deactivate")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Deactivate([FromRoute] Guid cvId)
        {
            await this.anonymousProfileService.Deactivate(cvId.ToString());

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
