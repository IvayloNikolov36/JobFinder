namespace JobFinder.Web.Controllers
{
    using JobFinder.Services;
    using JobFinder.Web.Models.JobAds;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using static JobFinder.Web.Infrastructure.WebConstants;

    public class JobAdsController : ApiController
    {
        private readonly IJobAdsService adsService;

        public JobAdsController(IJobAdsService adsService)
        {
            this.adsService = adsService;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<ActionResult<JobsListingsModel>> Get([FromQuery] JobAdsParams model)
        {
            (int totalCount, var jobAds) = await this.adsService.AllAsync<JobListingModel>(
                model.Page, model.Items, model.SearchText, model.EngagementId, model.CategoryId, 
                model.Location, model.SortBy, model.IsAscending);

            var resultModel = new JobsListingsModel
            {
                TotalCount = totalCount,
                JobAds = jobAds
            };

            return this.Ok(resultModel);
        }

        [HttpGet("details/{id}")]
        [Authorize]
        public async Task<ActionResult<JobAdDetailsModel>> Details(int id)
        {
            var jobDetails = await this.adsService.DetailsAsync<JobAdDetailsModel>(id);

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

            string publisherId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value; 

            await this.adsService
                .CreateAsync(publisherId, model.Position, model.Description, model.JobCategoryId,
                model.JobEngagementId, model.MinSalary, model.MaxSalary, model.Location);

            return this.Ok(new { Message = SuccessOnCreation });
        }

        [HttpPut("edit/{id}")]
        [Authorize]
        public async Task<ActionResult> Edit(int id, [FromBody] JobAdEditModel model)
        {
            //TODO: think about for editing expiration

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isEditDone = await this.adsService
                .EditAsync(id, userId, model.Position, model.Desription);

            if (!isEditDone)
            {
                return this.BadRequest(new { Message = CantEditAd });
            }

            return this.Ok(new { Message = UpdatedAd });
        }

        [HttpGet("engagements")]
        [Authorize(Roles = CompanyRole)]
        public async Task<ActionResult<IEnumerable<object>>> GetEngagements()
        {
            var engagements = await this.adsService.GetJobEngagements<JobEngagementViewModel>();

            return this.Ok(engagements);
        }

        [HttpGet("categories")]
        [Authorize(Roles = CompanyRole)]
        public async Task<ActionResult<IEnumerable<object>>> GetCategories()
        {
            var categories = await this.adsService.GetJobCategories<JobCategoryViewModel>();

            return this.Ok(categories);
        }
    }
}
