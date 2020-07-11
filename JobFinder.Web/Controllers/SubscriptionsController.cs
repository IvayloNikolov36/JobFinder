namespace JobFinder.Web.Controllers
{
    using JobFinder.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    public class SubscriptionsController : ApiController
    {
        private readonly ISubscriptionsService subscriptionsService;

        public SubscriptionsController(ISubscriptionsService subscriptionsService)
        {
            this.subscriptionsService = subscriptionsService;
        }

        [HttpGet("company/{id}")]
        public async Task<ActionResult> SubscribeToCompany(int id)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isSubscribed = await this.subscriptionsService.SubscribeToCompanyAsync(id, userId);
            if (!isSubscribed)
            {
                return this.BadRequest(new { Title = "Can't subscribe to company!" });
            }

            return this.Ok(new { Message = "Successfully subscribed for job ads from this company!" });
        }

        [HttpGet("unsubscribe/company/{id}")]
        public async Task<ActionResult> UnsubscribeFromCompany(int id)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isUnsubscribed = await this.subscriptionsService.UnsubscribeFromCompanyAsync(id, userId);
            if (!isUnsubscribed)
            {
                return this.BadRequest(new { Title = "Can't unsubscribe from company!" });
            }

            return this.Ok(new { Message = "Successfully unsubscribed for job ads from this company!" });
        }

        [HttpGet("jobCategory/{id}/{location}")]
        public async Task<ActionResult> SubscribeToJobCategory(int id, string location)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isSubscribed = await this.subscriptionsService.SubscribeToJobCategoryAsync(id, userId, location);
            if (!isSubscribed)
            {
                return this.BadRequest(new { Title = "Can't subscribe to job ads from this caregory!" });
            }

            return this.Ok(new { Message = "Successfully subscribed for job ads with chosen category!" });
        }

        [HttpGet("unsubscribe/jobCategory/{id}/{location}")]
        public async Task<ActionResult> UnsubscribeFromJobCategory(int id, string location)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isUnsubscribed = await this.subscriptionsService.UnsubscribeFromJobCategoryAsync(id, userId, location);
            if (!isUnsubscribed)
            {
                return this.BadRequest(new { Title = "Can't unsubscribe from job ads from this caregory!" });
            }

            return this.Ok(new { Message = "Successfully unsubscribed from job ads with chosen category!" });
        }

        [HttpGet("newJobAdsByCompany")]
        public async Task<ActionResult<IEnumerable<object>>> GetSubscribersNewJobAdsByCompany()
        {
            var data = await this.subscriptionsService.GetCompaniesNewJobAdsAsync();

            return this.Ok(data);
        }
       
        [HttpGet("newJobAdsByCategory")]
        public async Task<ActionResult<IEnumerable<object>>> GetSubscribersNewJobAdsByCategory()
        {
            var data = await this.subscriptionsService.GetNewJobAdsByCategoryAsync();

            return this.Ok(data);
        }
    }
}
