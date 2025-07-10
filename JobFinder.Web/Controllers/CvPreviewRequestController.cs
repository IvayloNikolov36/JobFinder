using JobFinder.Data.Models.Cv;
using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers;

[Authorize]
public class CvPreviewRequestController : ApiController
{
    private readonly ICvPreviewRequestService cvPreviewRequestService;

    public CvPreviewRequestController(
        ICvPreviewRequestService cvPreviewRequestService)
    {
        this.cvPreviewRequestService = cvPreviewRequestService;
    }

    [HttpGet]
    [Route("allow-cv-preview/{id:int}")]
    [Authorize(Roles = JobSeekerRole)]
    public async Task<IActionResult> AllowCvPreview([FromRoute] int id)
    {
        await this.cvPreviewRequestService.AllowCvPreview(id, this.User.GetCurrentUserId());

        return this.Ok();
    }

    [HttpGet]
    [Route("all-requests")]
    [Authorize(Roles = CompanyRole)]
    public async Task<IActionResult> GetAllCompanyCvRequests()
    {
        string userId = this.User.GetCurrentUserId();

        IEnumerable<CompanyCvPreviewRequestListingViewModel> cvRequests = await this.cvPreviewRequestService
            .GetAllCompanyCvPreviewRequests(userId);

        return this.Ok(cvRequests);
    }
}
