using JobFinder.Data.Models.Cv;
using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.CvModels;
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
    [Route("cv-requests")]
    [Authorize(Roles = JobSeekerRole)]
    public async Task<IActionResult> GetMyCvsPreviewRequests()
    {
        string userId = this.User.GetCurrentUserId();

        IEnumerable<CvPreviewRequestListingViewModel> cvRequests = await this.cvPreviewRequestService
            .GetAllCvPreviewRequests(userId);

        return this.Ok(cvRequests);
    }

    [HttpPost]
    [Route("cv-request")]
    [Authorize(Roles = CompanyRole)]
    [ServiceFilter(typeof(ValidateJobAdBelongsToUser))]
    [ServiceFilter(typeof(ValidateCompanyCanViewAnonymousProfile))]
    public async Task<IActionResult> MakeCvPreviewRequest(
        [FromBody] CvPreviewRequestCreateViewModel requestModel)
    {
        string currentUserId = this.User.GetCurrentUserId();

        await this.cvPreviewRequestService
            .MakeCvPreviewRequest(requestModel, currentUserId);

        return this.Ok();
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
