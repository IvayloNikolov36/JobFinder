using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv;

[Authorize]
[Route("api/work-experience")]
public class WorkExperiencesController : ApiController
{
    private readonly IWorkExperienceInfoService workExperienceService;

    public WorkExperiencesController(IWorkExperienceInfoService workExperienceService)
    {
        this.workExperienceService = workExperienceService;
    }

    [HttpPut]
    [Route("{cvId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateResult))]
    [Authorize(Roles = JobSeekerRole)]
    [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
    public async Task<IActionResult> Update(
        [FromRoute] Guid cvId,
        [FromBody] IEnumerable<WorkExperienceEditModel> workExperience)
    {
        UpdateResult result = await this.workExperienceService
            .UpdateAsync(cvId.ToString(), workExperience);

        return this.Ok(result);
    }
}
