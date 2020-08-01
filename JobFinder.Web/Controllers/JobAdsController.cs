namespace JobFinder.Web.Controllers
{
    using JobFinder.Services;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.JobAds;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using static JobFinder.Web.Infrastructure.WebConstants;

    [Authorize]
    public class JobAdsController : ApiController
    {
        private readonly IJobAdsService adsService;

        public JobAdsController(IJobAdsService adsService)
        {
            this.adsService = adsService;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "JobAdsCashProfile")]
        public async Task<ActionResult<DataListingsModel<JobListingModel>>> Get([FromQuery] JobAdsParams model)
        {
            (int totalCount, var jobAds) = await this.adsService.AllAsync<JobListingModel>(
                model.Page, model.Items, model.SearchText, model.EngagementId, model.CategoryId, 
                model.Location, model.SortBy, model.IsAscending);

            return this.Ok(new DataListingsModel<JobListingModel>(totalCount, jobAds));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobAdDetailsModel>> Details(int id)
        {
            var jobDetails = await this.adsService.GetAsync<JobAdDetailsModel>(id);

            if (jobDetails == null)
            {
                return this.NotFound(new { Message = NoJobFound });
            }

            return this.Ok(jobDetails);
        }

        [HttpPost]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> Create([FromBody] JobAdBindingModel model, [FromServices] ICompanyService companyService)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            int companyId = (await companyService.GetAsync(userId)).Id;

            await this.adsService
                .CreateAsync(companyId, model.Position, model.Description, model.JobCategoryId,
                model.JobEngagementId, model.MinSalary, model.MaxSalary, model.Location);

            return this.Ok(new { Message = SuccessOnCreation });
        }

        [HttpPut("{id}")]
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

    }
}
