using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Models.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    [Authorize]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService companiesService;

        public CompanyController(ICompanyService companiesService)
        {
            this.companiesService = companiesService;
        }

        [HttpGet]
        [Route("details/{companyId}")]
        public async Task<IActionResult> Details([FromRoute] int companyId)
        {
            CompanyDetailsUserViewModel details = await this.companiesService.Details(companyId, this.User.GetCurrentUserId());

            return this.Ok(details);
        }
    }
}
