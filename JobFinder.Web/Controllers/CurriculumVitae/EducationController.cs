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

    public class EducationController : BaseCVsController
    {
        private readonly IEducationService educationService;

        public EducationController(IEducationService educationService)
        {
            this.educationService = educationService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<EducationsListingModel>> GetAll([FromQuery] string cvId)
        {
            var educations = await this.educationService.AllAsync<EducationsListingModel>(cvId);

            return this.Ok(educations);
        }

        [HttpPost("create/{id}")]
        public async Task<ActionResult<IEnumerable<int>>> Create(string id, [FromBody] EducationInputModel[] models)
        {
            IList<int> entitiesIds = new List<int>();
            foreach (var model in models)
            {
                int educationId = await this.educationService
                    .CreateAsync(id, model.FromDate, model.ToDate,
                model.Location, model.EducationLevel, model.Major, model.MainSubjects);
                entitiesIds.Add(educationId);
            }
            

            return this.Ok(entitiesIds);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Edit([FromBody] EducationEditModel model)
        {
            bool isUpdated = await this.educationService.UpdateAsync(
                model.EducationId, model.FromDate, model.ToDate,
                model.Location, model.EducationLevel, model.Major, model.MainSubjects);

            if (!isUpdated)
            {
                return this.BadRequest(new { Title = "Education details are not updated!" });
            }

            return this.Ok(new { Message = "Education successfully updated!" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isDeleted = await this.educationService.DeleteAsync(id);

            if (!isDeleted)
            {
                return this.BadRequest(new { Title = "Education is not deleted!" });
            }

            return this.Ok(new { Message = "Education successfully deleted!" });
        }

        [HttpGet("levels")]
        public async Task<ActionResult> GetEducationLevels()
        {
            var educationLevels = new List<EnumTypeViewModel>();
            foreach (var level in Enum.GetValues(typeof(EducationLevel)))
            {
                educationLevels.Add(new EnumTypeViewModel((int)level, level.ToString()));
            }

            return this.Ok(educationLevels);
        }
    }
}
