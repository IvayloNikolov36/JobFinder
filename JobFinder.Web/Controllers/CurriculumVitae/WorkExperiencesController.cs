namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.CurriculumVitae;
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
        public async Task<ActionResult<IEnumerable<WorkExperienceListingModel>>> All([FromQuery] string cvId)
        {
            var workExperiences = await this.workExperienceService
                .AllAsync<WorkExperienceListingModel>(cvId);

            return this.Ok(workExperiences);
        }

        [HttpPost("{cvId:guid}")]
        [ServiceFilter(typeof(ValidateCvIdExistsServiceFilter))]
        public async Task<ActionResult<List<int>>> Create(Guid cvId, [FromBody] WorkExperienceInputModel[] experiences)
        {
            IList<int> entitiesIds = new List<int>();
            foreach (var model in experiences)
            {
                int workExperienceId = await this.workExperienceService
                    .CreateAsync(cvId.ToString(), model.FromDate, model.ToDate, model.JobTitle,model.Organization,
                    model.BusinessSector, model.Location, model.AdditionalDetails);

                entitiesIds.Add(workExperienceId);
            }

            return this.Ok(entitiesIds);
        }

        [HttpPut("{cvId:guid}/update")]
        public async Task<IActionResult> Update(
        [FromRoute] Guid cvId,
        [FromBody] IEnumerable<WorkExperienceEditModel> workExperience)
        {
            await this.workExperienceService.UpdateAsync(cvId.ToString(), workExperience);

            return this.Ok(new { Message = "Work Experience successfully updated!" });
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
