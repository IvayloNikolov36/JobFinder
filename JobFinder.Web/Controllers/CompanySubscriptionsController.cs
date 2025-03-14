namespace JobFinder.Web.Controllers
{
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Services;
    using JobFinder.Web.Infrastructure.Extensions;
    using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

            await this.companySubscriptionsService.SubscribeAsync(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/{id}")]
        public async Task<IActionResult> Unsubscribe([FromRoute] int id)
        {
            string userId = this.User.GetCurrentUserId();

            await this.companySubscriptionsService.UnsubscribeAsync(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/all")]
        public async Task<IActionResult> UnsubscribeAll()
        {
            string userId = this.User.GetCurrentUserId();

            await this.companySubscriptionsService.UnsubscribeAllAsync(userId);

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
        [Route("latestJobs/{recuringTypeId}")]
        public async Task<IActionResult> GetLatestJobs([FromRoute] int recuringTypeId)
        {
            IEnumerable<CompaniesSubscriptionsFunctionResult> data = await this.companySubscriptionsService.GetLatesJobAdsAsync(recuringTypeId);

            return this.Ok(data);
        }
    }
}
