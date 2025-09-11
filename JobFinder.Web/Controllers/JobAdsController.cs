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

        [HttpGet]
        [Route("{jobAdId:int}")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(JobAdDetailsViewModel))]
        [Authorize(Roles = CompanyRole)]
        [ServiceFilter(typeof(ValidateJobAdBelongsToUser))]
        public async Task<ActionResult<JobAdDetailsViewModel>> Get([FromRoute] int jobAdId)
        {
            JobAdDetailsViewModel jobDetails = await this.adsService.Get(jobAdId);

            return this.Ok(jobDetails);
        }

        [HttpPost]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(DataListingsModel<JobListingViewModel>))]
        public async Task<IActionResult> GetAllActive([FromBody] JobAdsFilterModel paramsModel)
        {
            DataListingsModel<JobListingViewModel> ads = await this.adsService.AllActiveAsync(paramsModel);

            return this.Ok(ads);
        }

        [HttpGet]
        [Route("company-ads/{companyId:int}")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<JobListingViewModel>))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> GetCompanyActiveAds([FromRoute] int companyId)
        {
            IEnumerable<JobListingViewModel> data = await this.adsService.AllActive(companyId);

            return this.Ok(data);
        }

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
                .GetCompanyAds(currentUserId);

            return this.Ok(jobAds);
        }

        [HttpGet]
        [Route("company/{lifecycleStatus:int?}")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<CompanyJobAdViewModel>))]
        public async Task<IActionResult> GetCompanyAds([FromRoute] int? lifecycleStatus)
        {
            string currentUserId = this.User.GetCurrentUserId();

            IEnumerable<CompanyJobAdViewModel> jobAds = await this.adsService
                .GetCompanyAds(currentUserId, lifecycleStatus);

            return this.Ok(jobAds);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(
            StatusCodes.Status201Created,
            Type = typeof(JobAdDetailsViewModel))]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> Create([FromBody] JobAdCreateViewModel model)
        {
            IdentityViewModel<int> result = await this.adsService
                .Create(model, this.User.GetCurrentUserId());

            return this.Ok(result);
        }

        [HttpPut]
        [Route("{jobAdId:int}")]
        [Authorize(Roles = CompanyRole)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(ValidateJobAdBelongsToUser))]
        public async Task<ActionResult> Update(
            [FromRoute] int jobAdId,
            [FromBody] JobAdEditModel model)
        {
            await this.adsService.Update(jobAdId, model);

            return this.Ok();
        }

        [HttpGet]
        [Route("{jobAdId:int}/retire")]
        [Authorize(Roles = CompanyRole)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(ValidateJobAdBelongsToUser))]
        public async Task<IActionResult> Retire([FromRoute] int jobAdId)
        {
            await this.adsService.Retire(jobAdId);

            return this.NoContent();
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
