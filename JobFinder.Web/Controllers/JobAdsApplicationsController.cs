using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.AdApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    [Route("api/ads-applications")]
    public class JobAdsApplicationsController : ApiController
    {
        private readonly IJobAdsApplicationsService jobAdsApplicationsService;

        public JobAdsApplicationsController(
            IJobAdsApplicationsService jobAdsApplicationsService)
        {
            this.jobAdsApplicationsService = jobAdsApplicationsService;
        }

        [HttpGet]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<JobAdApplicationViewModel>))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> GetAllMine()
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<JobAdApplicationViewModel> jobAdsApplications = await this.jobAdsApplicationsService
                .GetAllMine(currentUserId);

            return this.Ok(jobAdsApplications);
        }

        [HttpGet]
        [Route("{jobAdId:int}")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<JobApplicationInfoViewModel>))]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> GetJobAllApplications([FromRoute] int jobAdId)
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<JobApplicationInfoViewModel> jobApplications = await this.jobAdsApplicationsService
                .GetCompanyJobAdApplications(currentUserId, jobAdId);

            return this.Ok(jobApplications);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> Create(JobAdApplicationInputModel jobAdApplication)
        {
            jobAdApplication.ApplicantId = this.User.GetCurrentUserId();

            await this.jobAdsApplicationsService.Create(jobAdApplication);

            return this.Ok();
        }

        [HttpPost] // TODO: change to HTTP PATCH
        [Route("preview-info")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(PreviewInfoViewModel))]
        [Authorize(Roles = CompanyRole)]
        [ServiceFilter(typeof(ValidateCompanyAccessingCVSentForOwnAd))]
        public async Task<IActionResult> SetCvPreviewed(
            [FromBody] ApplicationPreviewInfoInputModel appModel)
        {
            PreviewInfoViewModel previewInfo = await this.jobAdsApplicationsService
                .SetPreviewInfo(appModel.CvId, appModel.JobAdId);

            return this.Ok(previewInfo);
        }
    }
}
