using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Subscribe([FromRoute] int id)
        {
            string userId = this.User.GetCurrentUserId();

            await this.companySubscriptionsService.Subscribe(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/{id}")]
        public async Task<IActionResult> Unsubscribe([FromRoute] int id)
        {
            string userId = this.User.GetCurrentUserId();

            await this.companySubscriptionsService.Unsubscribe(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/all")]
        public async Task<IActionResult> UnsubscribeAll()
        {
            string userId = this.User.GetCurrentUserId();

            await this.companySubscriptionsService.UnsubscribeAll(userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("mine")]
        public async Task<IActionResult> GetMySubscriptions()
        {
            string userId = this.User.GetCurrentUserId();

            IEnumerable<CompanySubscriptionViewModel> subscriptions = await this.companySubscriptionsService
                .GetMySubscriptions(userId);

            return this.Ok(subscriptions);
        }

        [HttpGet]
        [Route("latestJobs")]
        public async Task<IActionResult> GetLatestJobs()
        {
            IEnumerable<CompanyJobAdsForSubscribersViewModel> jobs = await this.companySubscriptionsService
                .GetLatesJobAds();

            return this.Ok(jobs);
        }
    }
}
