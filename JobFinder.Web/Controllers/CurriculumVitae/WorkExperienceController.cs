namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class WorkExperienceController : BaseCVsController
    {
        private readonly IWorkExperienceService workExperienceService;

        public WorkExperienceController(IWorkExperienceService workExperienceService)
        {
            this.workExperienceService = workExperienceService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<WorkExperiencesListingModel>>> All([FromQuery] string cvId)
        {
            var workExperiences = await this.workExperienceService
                .AllAsync<WorkExperiencesListingModel>(cvId);

            return this.Ok(workExperiences);
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> Create([FromBody] WorkExperienceInputModel model)
        {

            int workExperienceId = await this.workExperienceService.CreateAsync(model.CvId, model.FromDate, model.ToDate, model.JobTitle, 
                model.Organization, model.BusinessSector, model.Location, model.AdditionalDetails);

            return this.Ok(workExperienceId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] WorkExperienceEditModel model)
        {
            bool isUpdated = await this.workExperienceService.UpdateAsync(model.WorkExperienceId, model.FromDate, model.ToDate, model.JobTitle,
                model.Organization, model.BusinessSector, model.Location, model.AditionalDetails);

            if (!isUpdated)
            {
                return this.BadRequest(new { Message = "Work Experience not updated!" });
            }

            return this.Ok(new { Message = "Work Experience updated successfully!" } );
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isDeleted = await this.workExperienceService.DeleteAsync(id);
            if (!isDeleted)
            {
                return this.BadRequest(new { Message = "Work Experience not deleted!" });
            }

            return this.Ok(new { Message = "Work Experience successfully deleted!" });
        }
    }
}
