using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    [Route("api/company-subscriptions")]
    public class CompanySubscriptionsController : ApiController
    {
        private readonly ICompanySubscriptionsService companySubscriptionsService;

        public CompanySubscriptionsController(
            ICompanySubscriptionsService companySubscriptionsService)
        {
            this.companySubscriptionsService = companySubscriptionsService;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> Subscribe([FromRoute] int id)
        {
            string userId = this.User.GetCurrentUserId();

            await this.companySubscriptionsService.Subscribe(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> Unsubscribe([FromRoute] int id)
        {
            string userId = this.User.GetCurrentUserId();

            await this.companySubscriptionsService.Unsubscribe(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> UnsubscribeAll()
        {
            string userId = this.User.GetCurrentUserId();

            await this.companySubscriptionsService.UnsubscribeAll(userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("mine")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<CompanySubscriptionViewModel>))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> GetMySubscriptions()
        {
            string userId = this.User.GetCurrentUserId();

            IEnumerable<CompanySubscriptionViewModel> subscriptions = await this.companySubscriptionsService
                .GetMySubscriptions(userId);

            return this.Ok(subscriptions);
        }

        [HttpGet]
        [Route("latestJobs")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<CompanyJobAdsForSubscribersViewModel>))]
        public async Task<IActionResult> GetLatestJobs()
        {
            IEnumerable<CompanyJobAdsForSubscribersViewModel> jobs = await this.companySubscriptionsService
                .GetLatesJobAds();

            return this.Ok(jobs);
        }
    }
}
