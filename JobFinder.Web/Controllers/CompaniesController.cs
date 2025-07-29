using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            CompanyDetailsUserViewModel details = await this.companiesService
                .Details(id, this.User.GetCurrentUserId());

            return this.Ok(details);
        }
    }
}
