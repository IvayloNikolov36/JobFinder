namespace JobFinder.Web.Controllers
{
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Services;
    using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Security.Claims;
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
        public async Task<ActionResult> Subscribe([FromRoute] int id)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.companySubscriptionsService.SubscribeAsync(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("unsubscribe/{id}")]
        public async Task<ActionResult> Unsubscribe([FromRoute] int id)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.companySubscriptionsService.UnsubscribeAsync(id, userId);

            return this.Ok();
        }

        [HttpGet]
        [Route("mine")]
        public async Task<IActionResult> GetMySubscriptions()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            IEnumerable<CompanySubscriptionViewModel> subscriptions = await this.companySubscriptionsService
                .GetMySubscriptions(userId);

            return this.Ok(subscriptions);
        }

        [HttpGet]
        [Route("latestJobs")]
        public async Task<IActionResult> GetLatestJobs()
        {
            IEnumerable<CompaniesSubscriptionsData> data = await this.companySubscriptionsService.GetLatesJobAdsAsync();

            return this.Ok(data);
        }
    }
}
