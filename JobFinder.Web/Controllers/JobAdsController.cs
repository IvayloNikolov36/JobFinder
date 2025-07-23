using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.AnonymousProfile;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.JobAds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    public class JobAdsController : ApiController
    {
        private readonly IJobAdsService adsService;

        public JobAdsController(IJobAdsService adsService)
        {
            this.adsService = adsService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllActive([FromBody] JobAdsFilterModel paramsModel)
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

            IEnumerable<CompanyJobAdViewModel> jobAds = await this.adsService
                .GetAllCompanyAds(currentUserId);

            return this.Ok(jobAds);
        }

        [HttpGet]
        [Route("company/{active:bool}")]
        public async Task<IActionResult> GetCompanyAds([FromRoute] bool active)
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<CompanyJobAdViewModel> jobAds = await this.adsService
                .GetCompanyAds(currentUserId, active);

            return this.Ok(jobAds);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<JobAdDetailsViewModel>> Details([FromRoute] int id)
        {
            JobAdDetailsViewModel jobDetails = await this.adsService.Get(id);

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

            await this.adsService.Create(model, companyId);

            return this.Ok();
        }

        [HttpPut]
        [Route("{jobAdId:int}")]
        [Authorize(Roles = CompanyRole)]
        [ServiceFilter(typeof(ValidateJobAdBelongsToUser))]
        public async Task<ActionResult> Update([FromRoute] int jobAdId, [FromBody] JobAdEditModel model)
        {
            string userId = this.User.GetCurrentUserId();

            await this.adsService.Update(jobAdId, userId, model);

            return this.Ok();
        }

        [HttpGet]
        [Route("deactivate")]
        public async Task<IActionResult> DeactivateAds()
        {
            await this.adsService.DeactivateAds();

            return this.Ok();
        }

        [HttpGet]
        [Route("{jobAdId:int}/anonymous-profiles")]
        [Authorize(Roles = CompanyRole)]
        [ServiceFilter(typeof(ValidateJobAdBelongsToUser))]
        public async Task<IActionResult> GetRelevantAnonymousProfiles([FromRoute] int jobAdId)
        {
            IEnumerable<AnonymousProfileListingViewModel> anonymousProfiles = await this.adsService
                .GetRelevantAnonymousProfiles(jobAdId);

            return this.Ok(anonymousProfiles);
        }
    }
}
