using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    [Route("api/companies")]
    public class CompaniesController : ApiController
    {
        private readonly ICompaniesService companiesService;

        public CompaniesController(ICompaniesService companiesService)
        {
            this.companiesService = companiesService;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDetailsUserViewModel))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            CompanyDetailsUserViewModel details = await this.companiesService
                .Details(id, this.User.GetCurrentUserId());

            return this.Ok(details);
        }

        [HttpGet]
        [Route("{id:int}/ads")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyJobAdsListingViewModel))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<IActionResult> GetCompanyActiveAds([FromRoute] int id)
        {
            CompanyJobAdsListingViewModel data = await this.companiesService.AllActiveAds(id);

            return this.Ok(data);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<CompanyListingViewModel>))
        ]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<CompanyListingViewModel> companiesData = await this.companiesService
                .GetAll();

            return this.Ok(companiesData);
        }
    }
}
