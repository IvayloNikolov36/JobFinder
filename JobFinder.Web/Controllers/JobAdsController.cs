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
        public async Task<ActionResult<DataListingsModel<JobListingModel>>> Get([FromQuery] JobAdsParams paramsModel)
        {
            DataListingsModel<JobListingModel> data = await this.adsService.AllAsync(paramsModel);

            return this.Ok(data);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<JobAdDetailsModel>> Details([FromRoute] int id)
        {
            JobAdDetailsModel jobDetails = await this.adsService.GetAsync<JobAdDetailsModel>(id);

            if (jobDetails == null)
            {
                return this.NotFound(new { Message = NoJobFound });
            }

            return this.Ok(jobDetails);
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> Create(
            [FromBody] JobAdCreateModel model,
            [FromServices] ICompanyService companyService)
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            int companyId = (await companyService.GetAsync(userId)).Id;

            await this.adsService.CreateAsync(companyId, model);

            return this.Ok(new { Message = SuccessOnCreation });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromBody] JobAdEditModel model)
        {
            // TODO: think about editing expiration

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool isEditDone = await this.adsService
                .EditAsync(id, userId, model.Position, model.Description);

            if (!isEditDone)
            {
                return this.BadRequest(new { Message = CantEditAd });
            }

            return this.Ok(new { Message = UpdatedAd });
        }

    }
}
