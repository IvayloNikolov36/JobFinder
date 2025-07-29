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
    [Route("api/ads")]
    public class JobAdsController : ApiController
    {
        private readonly IJobAdsService adsService;

        public JobAdsController(IJobAdsService adsService)
        {
            this.adsService = adsService;
        }

        [HttpPost]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(DataListingsModel<JobListingModel>))]
        public async Task<IActionResult> GetAllActive([FromBody] JobAdsFilterModel paramsModel)
        {
            DataListingsModel<JobListingModel> ads = await this.adsService.AllActiveAsync(paramsModel);

            return this.Ok(ads);
        }

        // TODO: create an endpoint for job seeker to access company ads

        [HttpGet]
        [Route("company/all")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<CompanyJobAdViewModel>))]
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
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<CompanyJobAdViewModel>))]
        public async Task<IActionResult> GetCompanyAds([FromRoute] bool active)
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<CompanyJobAdViewModel> jobAds = await this.adsService
                .GetCompanyAds(currentUserId, active);

            return this.Ok(jobAds);
        }

        [HttpGet]
        [Route("{id:int}", Name = "Details")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(JobAdDetailsViewModel))]
        public async Task<ActionResult<JobAdDetailsViewModel>> GetDetails([FromRoute] int id)
        {
            JobAdDetailsViewModel jobDetails = await this.adsService.Get(id);

            return this.Ok(jobDetails);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(
            StatusCodes.Status201Created,
            Type = typeof(JobAdDetailsViewModel))]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> Create(
            [FromBody] JobAdCreateViewModel model,
            [FromServices] ICompaniesService companyService)
        {
            string userId = this.User.GetCurrentUserId();

            int companyId = await companyService.GetCompanyId(userId);

            int id = await this.adsService.Create(model, companyId);

            IdentityViewModel<int> result = new(id);

            return this.CreatedAtRoute("Details", result, result);
        }

        [HttpPut]
        [Route("{jobAdId:int}")]
        [Authorize(Roles = CompanyRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(ValidateJobAdBelongsToUser))]
        public async Task<ActionResult> Update([FromRoute] int jobAdId, [FromBody] JobAdEditModel model)
        {
            string userId = this.User.GetCurrentUserId();

            await this.adsService.Update(jobAdId, userId, model);

            return this.Ok();
        }

        [HttpGet]
        [Route("deactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeactivateAds()
        {
            await this.adsService.DeactivateAds();

            return this.Ok();
        }

        [HttpGet]
        [Route("{jobAdId:int}/anonymous-profiles")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<AnonymousProfileListingViewModel>))]
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
