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
[Route("api/cv-requests")]
public class CvPreviewRequestController : ApiController
{
    private readonly ICvPreviewRequestService cvPreviewRequestService;

    public CvPreviewRequestController(
        ICvPreviewRequestService cvPreviewRequestService)
    {
        this.cvPreviewRequestService = cvPreviewRequestService;
    }

    [HttpGet]
    [ProducesResponseType(
        StatusCodes.Status200OK,
        Type = typeof(IEnumerable<CvPreviewRequestListingViewModel>))]
    [Authorize(Roles = JobSeekerRole)]
    public async Task<IActionResult> GetMyCvsPreviewRequests()
    {
        string userId = this.User.GetCurrentUserId();

        IEnumerable<CvPreviewRequestListingViewModel> cvRequests = await this.cvPreviewRequestService
            .GetAllCvPreviewRequests(userId);

        return this.Ok(cvRequests);
    }

    [HttpPost]
    [Authorize(Roles = CompanyRole)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ServiceFilter(typeof(ValidateJobAdBelongsToUser))]
    [ServiceFilter(typeof(ValidateCompanyCanViewAnonymousProfile))]
    public async Task<IActionResult> CreateCvPreviewRequest(
        [FromBody] CvPreviewRequestCreateViewModel requestModel)
    {
        string currentUserId = this.User.GetCurrentUserId();

        await this.cvPreviewRequestService
            .MakeCvPreviewRequest(requestModel, currentUserId);

        return this.Ok();
    }

    [HttpGet]
    [Route("{id:int}/accept")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = JobSeekerRole)]
    public async Task<IActionResult> AcceptCvPreviewRequest([FromRoute] int id)
    {
        await this.cvPreviewRequestService.AcceptCvPreviewRequest(
            id,
            this.User.GetCurrentUserId());

        return this.Ok();
    }

    [HttpGet]
    [Route("all")]
    [ProducesResponseType(
            StatusCodes.Status200OK,
            Type = typeof(IEnumerable<CompanyCvPreviewRequestListingViewModel>))]
    [Authorize(Roles = CompanyRole)]
    public async Task<IActionResult> GetAllCompanyCvRequests()
    {
        string userId = this.User.GetCurrentUserId();

        IEnumerable<CompanyCvPreviewRequestListingViewModel> cvRequests = await this.cvPreviewRequestService
            .GetAllCompanyCvPreviewRequests(userId);

        return this.Ok(cvRequests);
    }
}
