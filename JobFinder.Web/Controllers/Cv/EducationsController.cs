using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers.Cv
{
    public class EducationsController : BaseCVsController
    {
        private readonly IEducationsInfoService educationService;

        public EducationsController(IEducationsInfoService educationService)
        {
            this.educationService = educationService;
        }

        [HttpPut("{cvId:guid}/update")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<EducationEditModel> educationsModel)
        {
            UpdateResult result = await this.educationService.Update(cvId.ToString(), educationsModel);

            return this.Ok(result);
        }
    }
}
