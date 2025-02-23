using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.JobAds;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [Route("jobAd/{id}")]
        public async Task<IActionResult> GetJobApplications([FromRoute] int id)
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<JobAdApplicationViewModel> jobAdsApplications = await this.jobAdsApplicationsService
                .GetJobAdApplications(id, currentUserId);

            return this.Ok(jobAdsApplications);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobAdApplicationInputModel jobAdApplication)
        {
            jobAdApplication.ApplicantId = this.User.GetCurrentUserId();

            await this.jobAdsApplicationsService.Create(jobAdApplication);

            return this.Ok();
        }
    }
}
