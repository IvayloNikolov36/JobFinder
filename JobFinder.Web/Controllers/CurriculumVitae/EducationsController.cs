namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CV;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class EducationsController : BaseCVsController
    {
        private readonly IEducationService educationService;

        public EducationsController(IEducationService educationService)
        {
            this.educationService = educationService;
        }

        [HttpGet]
        public async Task<ActionResult<EducationViewModel>> GetAll([FromQuery] string cvId)
        {
            var educations = await this.educationService.AllAsync<EducationViewModel>(cvId);

            return this.Ok(educations);
        }

        [HttpPut("{cvId:guid}/update")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<EducationEditModel> educationsModel)
        {
            await this.educationService.UpdateAsync(cvId.ToString(), educationsModel);

            return this.Ok(new { Message = "Educations successfully updated!" });
        }
    }
}
