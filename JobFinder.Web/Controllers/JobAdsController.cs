using JobFinder.Data.Models;
using JobFinder.Services;
using JobFinder.Services.Models;
using JobFinder.Web.Models.JobAds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    public class JobAdsController : ApiController
    {
        private readonly IJobAdsService adsService;

        public JobAdsController(IJobAdsService adsService)
        {
            this.adsService = adsService;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<JobsListingServiceModel>> Get([FromQuery] JobAdsParams model)
        {
            var jobsListingModel = await this.adsService.AllAsync(
                model.Page, model.Items, model.SearchText, model.EngagementId, model.CategoryId, model.Location,
                model.SortBy, model.IsAscending);

            return this.Ok(jobsListingModel);
        }

        [HttpGet("details/{id}")]
        [Authorize]
        public async Task<ActionResult<JobAd>> Details(int id)
        {
            var jobDetails = await this.adsService.GetDetailsAsync(id);

            if (jobDetails == null)
            {
                return this.NotFound(new { Message = NoJobFound });
            }

            return this.Ok(jobDetails);
        }

        [HttpPost("create")]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> Create([FromBody] JobAdBindingModel model)
        {
            //TODO: make filter to check for valid jobCategoryId and jobEngagementId

            DateTime expirationDate = DateTime.UtcNow.AddDays(model.DaysActive);

            string publisherId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.adsService
                .CreateAsync(publisherId, model.Position, model.Description, expirationDate, model.JobCategoryId,
                model.JobEngagementId, model.MinSalary, model.MaxSalary, model.Location);

            return this.Ok(new { Message = SuccessOnCreation });
        }

        [HttpPut("edit/{id}")]
        [Authorize]
        public async Task<ActionResult> Edit(int id, [FromBody] JobAdEditModel model)
        {
            var offerFromDb = await this.adsService.GetAsync(id);

            if (offerFromDb == null)
            {
                return this.NotFound(new { Message = NoJobFound });
            }

            //TODO: make filters!
            //TODO: think about for editing expiration

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != offerFromDb.PublisherId)
            {
                return this.BadRequest(new { Message = CantEditAd });
            }

            await this.adsService
                .EditAsync(id, model.Position, model.Desription, model.DaysActive);

            return this.Ok(new { Message = UpdatedAd });
        }

        [HttpGet("engagements")]
        [Authorize(Roles = CompanyRole)]
        public async Task<ActionResult<IEnumerable<object>>> GetEngagements()
        {
            var dbEngagements = await this.adsService.GetJobEngagements();

            var engagements = dbEngagements.Select(e => new
            {
                Id = e.Key,
                Name = e.Value
            }).ToList();

            return this.Ok(engagements);
        }

        [HttpGet("categories")]
        [Authorize(Roles = CompanyRole)]
        public async Task<ActionResult<IEnumerable<object>>> GetCategories()
        {
            var dbCategories = await this.adsService.GetJobCategories();

            var categories = dbCategories.Select(c => new
            {
                Id = c.Key,
                Name = c.Value
            }).ToList();

            return this.Ok(categories);
        }
    }
}
