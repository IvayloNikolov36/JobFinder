using JobFinder.Services;
using JobFinder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers;

[Authorize]
public class CvPreviewRequestController : ApiController
{
    private readonly ICvPreviewRequestService cvPreviewRequestService;

    public CvPreviewRequestController(ICvPreviewRequestService cvPreviewRequestService)
    {
        this.cvPreviewRequestService = cvPreviewRequestService;
    }

    [HttpGet]
    [Route("allow-cv-preview/{id:int}")]
    public async Task<IActionResult> AllowCvPreview([FromRoute] int id)
    {
        await this.cvPreviewRequestService.AllowCvPreview(id, this.User.GetCurrentUserId());

        return this.Ok();
    }
}
