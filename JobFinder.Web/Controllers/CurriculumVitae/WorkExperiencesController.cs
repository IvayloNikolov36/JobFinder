namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CV;
    using JobFinder.Web.Infrastructure.Filters;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class WorkExperiencesController : BaseCVsController
    {
        private readonly IWorkExperienceInfosService workExperienceService;

        public WorkExperiencesController(IWorkExperienceInfosService workExperienceService)
        {
            this.workExperienceService = workExperienceService;
        }

        [HttpPut("{cvId:guid}/update")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
        [FromRoute] Guid cvId,
        [FromBody] IEnumerable<WorkExperienceEditModel> workExperience)
        {
            UpdateResult result = await this.workExperienceService.UpdateAsync(cvId.ToString(), workExperience);

            return this.Ok(result);
        }
    }
}
