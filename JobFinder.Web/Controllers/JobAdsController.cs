namespace JobFinder.Web.Controllers
{
    using JobFinder.Services;
    using JobFinder.Web.Infrastructure.Extensions;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.JobAds;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
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

        [HttpPost]
        public async Task<IActionResult> AllActiveAds([FromBody] JobAdsFilterModel paramsModel)
        {
            DataListingsModel<JobListingModel> ads = await this.adsService.AllActiveAsync(paramsModel);

            return this.Ok(ads);
        }

        [HttpGet]
        [Route("company/all")]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> GetAllCompanyAds()
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<CompanyJobAdViewModel> jobAds = await this.adsService.GetAllCompanyAds(currentUserId);

            return this.Ok(jobAds);
        }

        [HttpGet]
        [Route("company/{active}")]
        public async Task<IActionResult> GetCompanyAds([FromRoute] bool active)
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<CompanyJobAdViewModel> jobAds = await this.adsService.GetCompanyAds(currentUserId, active);

            return this.Ok(jobAds);
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
            [FromBody] JobAdCreateViewModel model,
            [FromServices] ICompanyService companyService)
        {
            string userId = this.User.GetCurrentUserId();

            int companyId = await companyService.GetCompanyId(userId);

            await this.adsService.CreateAsync(companyId, model);

            return this.Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromBody] JobAdEditModel model)
        {
            string userId = this.User.GetCurrentUserId();

            await this.adsService.EditAsync(id, userId, model);

            return this.Ok();
        }

        [HttpGet]
        [Route("deactivate")]
        public async Task<IActionResult> DeactivateAds()
        {
            await this.adsService.DeactivateAds();

            return this.Ok();
        }
    }
}
