namespace JobFinder.Web.Controllers
{
    using JobFinder.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
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


        [HttpGet("{id}")]
        public async Task<ActionResult> Subscribe(int id)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isSubscribed;
            try
            {
                isSubscribed = await this.companySubscriptionsService.SubscribeAsync(id, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.BadRequest(new { Title = "Can't subscribe twice to this company!" });
            }

            if (!isSubscribed)
            {
                return this.BadRequest(new { Title = "Can't subscribe to unexisting company!" });
            }

            return this.Ok(new { Message = "Successfully subscribed for job ads from this company!" });
        }

        [HttpGet("unsubscribe/{id}")]
        public async Task<ActionResult> Unsubscribe(int id)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isUnsubscribed = await this.companySubscriptionsService.UnsubscribeAsync(id, userId);

            if (!isUnsubscribed)
            {
                return this.BadRequest(new { Title = "You doesn't have un subscription to this company!" });
            }

            return this.Ok(new { Message = "Successfully unsubscribed for job ads from this company!" });
        }


        [HttpGet("latestJobs")]
        public async Task<ActionResult<IEnumerable<object>>> GetLatestJobs()
        {
            var data = await this.companySubscriptionsService.GetLatesJobAdsAsync();

            return this.Ok(data);
        }
    }
}
