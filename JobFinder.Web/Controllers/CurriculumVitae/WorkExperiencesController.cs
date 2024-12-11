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
        private readonly IWorkExperienceService workExperienceService;

        public WorkExperiencesController(IWorkExperienceService workExperienceService)
        {
            this.workExperienceService = workExperienceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkExperienceViewModel>>> All([FromQuery] string cvId)
        {
            var workExperiences = await this.workExperienceService
                .AllAsync<WorkExperienceViewModel>(cvId);

            return this.Ok(workExperiences);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await this.workExperienceService.DeleteAsync(id);
            if (!isDeleted)
            {
                return this.BadRequest(new { Title = "Work Experience not deleted!" });
            }

            return this.Ok(new { Message = "Work Experience successfully deleted!" });
        }

    }
}
