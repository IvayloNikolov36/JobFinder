using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.AdApplication;
using JobFinder.Web.Models.JobAds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    public class JobAdsApplicationsController : ApiController
    {
        private readonly IJobAdsApplicationsService jobAdsApplicationsService;

        public JobAdsApplicationsController(IJobAdsApplicationsService jobAdsApplicationsService)
        {
            this.jobAdsApplicationsService = jobAdsApplicationsService;
        }

        [HttpGet]
        [Route("mine")]
        public async Task<IActionResult> GetAllMine()
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<JobAdApplicationViewModel> jobAdsApplications = await this.jobAdsApplicationsService
                .GetAllMine(currentUserId);

            return this.Ok(jobAdsApplications);
        }

        [HttpGet]
        [Route("{jobAdId}")]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> GetJobAllApplications([FromRoute] int jobAdId)
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<JobApplicationInfoViewModel> jobApplications = await this.jobAdsApplicationsService
                .GetCompanyJobAdApplications(currentUserId, jobAdId);

            return this.Ok(jobApplications);
        }

        [HttpGet]
        [Route("jobAd/{id}")]
        public async Task<IActionResult> GetUserJobsApplications([FromRoute] int id)
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<JobAdApplicationViewModel> jobAdsApplications = await this.jobAdsApplicationsService
                .GetUserJobsAdApplications(currentUserId, id);

            return this.Ok(jobAdsApplications);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobAdApplicationInputModel jobAdApplication)
        {
            jobAdApplication.ApplicantId = this.User.GetCurrentUserId();

            await this.jobAdsApplicationsService.Create(jobAdApplication);

            return this.Ok();
        }

        [HttpPost]
        [Route("preview-info")]
        [Authorize(Roles = CompanyRole)]
        [ServiceFilter(typeof(ValidateCompanyAccessingCVSentForOwnAd))]
        public async Task<IActionResult> SetCvPreviewed([FromBody] ApplicationPreviewInfoInputModel appModel)
        {
            PreviewInfoViewModel previewInfo = await this.jobAdsApplicationsService
                .SetPreviewInfo(appModel.CvId, appModel.JobAdId);

            return this.Ok(previewInfo);
        }
    }
}
