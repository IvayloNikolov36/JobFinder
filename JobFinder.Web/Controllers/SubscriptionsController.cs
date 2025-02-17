namespace JobFinder.Web.Controllers
{
    using JobFinder.Services;
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

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
        public async Task<IActionResult> SubscribeForJobs([FromBody] JobSubscriptionCriteriasViewModel subscription)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.subscriptionsService
                .SubscribeForJobs(userId, subscription.JobCategoryId, subscription.Location);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/{id}")]
        public async Task<IActionResult> UnsubscribeForJobs([FromRoute] int id)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.subscriptionsService.UnsubscribeFromJobsWithCriterias(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/all")]
        public async Task<IActionResult> UnsubscribeFromAll()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.subscriptionsService.UnsubscribeFromAllJobsWithCriterias(userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("mine")]
        public async Task<IActionResult> GetMyJobSubscriptions()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            IEnumerable<JobSubscriptionViewModel> subscriptions = await this.subscriptionsService.GetAllJobSubscriptions(userId);

            return this.Ok(subscriptions);
        }
      
        [HttpGet]
        [Route("newJobAdsByCategory")]
        public async Task<IActionResult> GetSubscribersNewJobAdsByCategory()
        {
            IEnumerable<JobAdsByCategoryAndLocationViewModel> data = await this.subscriptionsService.GetNewJobAdsByCategoryAsync();

            return this.Ok(data);
        }
    }
}
