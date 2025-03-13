namespace JobFinder.Web.Controllers
{
    using JobFinder.Services;
    using JobFinder.Web.Infrastructure.Extensions;
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize]
    public class SubscriptionsController : ApiController
    {
        private readonly ISubscriptionsService subscriptionsService;

        public SubscriptionsController(ISubscriptionsService subscriptionsService)
        {
            this.subscriptionsService = subscriptionsService;
        }

        [HttpPost]
        [Route("subscribe")]
        public async Task<IActionResult> SubscribeForJobs([FromBody] JobSubscriptionCriteriasViewModel subscription)
        {
            string userId = this.User.GetCurrentUserId();

            JobSubscriptionViewModel createdSubscription = await this.subscriptionsService.SubscribeForJobs(userId, subscription);

            return this.Ok(createdSubscription);
        }

        [HttpGet]
        [Route("unsubscribe/{id}")]
        public async Task<IActionResult> UnsubscribeFromJobs([FromRoute] int id)
        {
            string userId = this.User.GetCurrentUserId();

            await this.subscriptionsService.UnsubscribeFromJobsWithCriterias(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/all")]
        public async Task<IActionResult> UnsubscribeFromAllJobs()
        {
            string userId = this.User.GetCurrentUserId();

            await this.subscriptionsService.UnsubscribeFromAllJobsWithCriterias(userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("mine")]
        public async Task<IActionResult> GetMyJobSubscriptions()
        {
            string userId = this.User.GetCurrentUserId();

            IEnumerable<JobSubscriptionViewModel> subscriptions = await this.subscriptionsService.GetAllJobSubscriptions(userId);

            return this.Ok(subscriptions);
        }
      
        [HttpGet]
        [Route("new-ads")]
        public async Task<IActionResult> GetSubscribersNewJobAds()
        {
            IEnumerable<JobAdsSubscriptionsViewModel> data = await this.subscriptionsService.GetLatestJobAdsAsync();

            return this.Ok(data);
        }
    }
}
