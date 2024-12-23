﻿namespace JobFinder.Web.Controllers
{
    using JobFinder.Services;
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
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

        [HttpGet]
        [Route("jobCategory/{id}/{location}")]
        public async Task<IActionResult> SubscribeToJobCategory([FromRoute] int id, [FromRoute] string location)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isSubscribed;

            try
            {
                isSubscribed = await this.subscriptionsService.SubscribeToJobCategoryAsync(id, userId, location);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return this.BadRequest(new { Title = "You already have a subscription for this category and location!" });
            }
            
            if (!isSubscribed)
            {
                return this.BadRequest(new { Title = "Invalid job category!" });
            }

            return this.Ok(new { Message = "Successfully subscribed for job ads with chosen category!" });
        }

        [HttpGet]
        [Route("unsubscribe/jobCategory/{id}/{location}")]
        public async Task<IActionResult> UnsubscribeFromJobCategory([FromRoute] int id, [FromRoute] string location)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isUnsubscribed = await this.subscriptionsService
                .UnsubscribeFromJobCategoryAsync(id, userId, location);

            if (!isUnsubscribed)
            {
                return this.BadRequest(new { Title = "Can't unsubscribe from job ads from this caregory!" });
            }

            return this.Ok(new { Message = "Successfully unsubscribed from job ads with chosen category!" });
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
