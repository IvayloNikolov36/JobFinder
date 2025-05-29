using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CVModels;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers.CurriculumVitae
{
    public class CoursesController : BaseCVsController
    {
        private readonly ICoursesCertificatesService coursesService;

        public CoursesController(ICoursesCertificatesService coursesService)
        {
            this.coursesService = coursesService;
        }

        [HttpPut]
        [Route("{cvId:guid}/update")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<CourseSertificateEditModel> coursesInfo)
        {
            UpdateResult result = await this.coursesService.Update(cvId.ToString(), coursesInfo);

            return this.Ok(result);
        }
    }
}
