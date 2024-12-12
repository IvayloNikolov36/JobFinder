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

    public class EducationsController : BaseCVsController
    {
        private readonly IEducationInfosService educationService;

        public EducationsController(IEducationInfosService educationService)
        {
            this.educationService = educationService;
        }

        [HttpPut("{cvId:guid}/update")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<EducationEditModel> educationsModel)
        {
            UpdateResult result = await this.educationService.UpdateAsync(cvId.ToString(), educationsModel);

            return this.Ok(result);
        }
    }
}
