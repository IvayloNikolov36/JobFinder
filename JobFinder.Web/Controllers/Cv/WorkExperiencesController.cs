using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv;

public class WorkExperiencesController : BaseCVsController
{
    private readonly IWorkExperienceInfosService workExperienceService;

    public WorkExperiencesController(IWorkExperienceInfosService workExperienceService)
    {
        this.workExperienceService = workExperienceService;
    }

    [HttpPut]
    [Route("{cvId:guid}/update")]
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
