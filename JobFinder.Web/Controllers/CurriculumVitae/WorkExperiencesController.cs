namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CurriculumVitae;
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

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<WorkExperienceListingModel>>> All([FromQuery] string cvId)
        {
            var workExperiences = await this.workExperienceService
                .AllAsync<WorkExperienceListingModel>(cvId);

            return this.Ok(workExperiences);
        }

        [HttpPost("create/{id}")]
        public async Task<ActionResult<List<int>>> Create(string id, [FromBody] WorkExperienceInputModel[] experiences)
        {
            IList<int> entitiesIds = new List<int>();
            foreach (var model in experiences)
            {
                int workExperienceId = await this.workExperienceService
                    .CreateAsync(id, model.FromDate, model.ToDate, model.JobTitle,model.Organization,
                    model.BusinessSector, model.Location, model.AdditionalDetails);

                entitiesIds.Add(workExperienceId);
            }

            return this.Ok(entitiesIds);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] WorkExperienceEditModel model)
        {
            bool isUpdated = await this.workExperienceService.UpdateAsync(model.WorkExperienceId, model.FromDate, model.ToDate, model.JobTitle,
                model.Organization, model.BusinessSector, model.Location, model.AditionalDetails);

            if (!isUpdated)
            {
                return this.BadRequest(new { Title = "Work Experience not updated!" });
            }

            return this.Ok(new { Message = "Work Experience updated successfully!" } );
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isDeleted = await this.workExperienceService.DeleteAsync(id);
            if (!isDeleted)
            {
                return this.BadRequest(new { Title = "Work Experience not deleted!" });
            }

            return this.Ok(new { Message = "Work Experience successfully deleted!" });
        }

        [HttpGet("businessSectors")]
        public IActionResult GetBusinessSectors()
        {
            var sectors = new List<EnumTypeViewModel>();
            foreach (var sector in Enum.GetValues(typeof(BusinessSector)))
            {
                sectors.Add(new EnumTypeViewModel((int)sector, sector.ToString()));
            }

            return this.Ok(sectors);
        }
    }
}
