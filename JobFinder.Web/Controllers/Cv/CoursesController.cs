using JobFinder.Services.Cv;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers.Cv
{
    public class CoursesController : BaseCVsController
    {
        private readonly ICoursesInfoService coursesService;

        public CoursesController(ICoursesInfoService coursesService)
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
