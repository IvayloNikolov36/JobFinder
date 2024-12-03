namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
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
        public async Task<ActionResult<EducationListingModel>> GetAll([FromQuery] string cvId)
        {
            var educations = await this.educationService.AllAsync<EducationListingModel>(cvId);

            return this.Ok(educations);
        }

        // TODO: delete?
        //[HttpPost("{cvId:guid}")]
        //public async Task<ActionResult<IEnumerable<int>>> Create(Guid cvId, [FromBody] EducationInputModel[] models)
        //{
        //    IList<int> entitiesIds = new List<int>();
        //    foreach (var model in models)
        //    {
        //        int educationId = await this.educationService.CreateAsync(
        //            cvId.ToString(), model.FromDate, model.ToDate, model.Organization,
        //            model.Location, model.EducationLevel, model.Major, model.MainSubjects);

        //            entitiesIds.Add(educationId);
        //    }

        //    return this.Ok(entitiesIds);
        //}

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
