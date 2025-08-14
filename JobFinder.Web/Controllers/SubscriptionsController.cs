using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    public class SubscriptionsController : ApiController
    {
        private readonly ISubscriptionsService subscriptionsService;

        public SubscriptionsController(
            ISubscriptionsService subscriptionsService)
        {
            this.subscriptionsService = subscriptionsService;
        }

        [HttpPost]
        [Route("subscribe")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JobSubscriptionViewModel))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> SubscribeForJobs(
            [FromBody] JobSubscriptionCriteriasViewModel subscription)
        {
            string userId = this.User.GetCurrentUserId();

            JobSubscriptionViewModel createdSubscription = await this.subscriptionsService
                .SubscribeForJobs(userId, subscription);

            return this.Ok(createdSubscription);
        }

        [HttpGet]
        [Route("unsubscribe/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> UnsubscribeFromJobs([FromRoute] int id)
        {
            string userId = this.User.GetCurrentUserId();

            await this.subscriptionsService
                .UnsubscribeFromJobsWithCriterias(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> UnsubscribeFromAllJobs()
        {
            string userId = this.User.GetCurrentUserId();

            await this.subscriptionsService
                .UnsubscribeFromAllJobsWithCriterias(userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("mine")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<JobSubscriptionViewModel>))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> GetMyJobSubscriptions()
        {
            string userId = this.User.GetCurrentUserId();

            IEnumerable<JobSubscriptionViewModel> subscriptions = await this.subscriptionsService
                .GetAllJobSubscriptions(userId);

            return this.Ok(subscriptions);
        }

        [HttpGet]
        [Route("new-ads/{recurringTypeId:int}")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IDictionary<string, List<JobAdsSubscriptionsViewModel>>))]
        public async Task<IActionResult> GetLatestJobAdsForSubscribers([FromRoute] int recurringTypeId)
        {
            IDictionary<string, List<JobAdsSubscriptionsViewModel>> data = await this.subscriptionsService
                .GetLatestJobAdsAsync(recurringTypeId);

            return this.Ok(data);
        }
    }
}
