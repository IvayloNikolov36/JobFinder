using JobFinder.Services.Cv;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv
{
    [Authorize]
    [Route("api/courses-info")]
    public class CoursesController : ApiController
    {
        private readonly ICoursesInfoService coursesService;

        public CoursesController(ICoursesInfoService coursesService)
        {
            this.coursesService = coursesService;
        }

        [HttpPut]
        [Route("{cvId:guid}")]
        [Authorize(Roles = JobSeekerRole)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateResult))]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<CourseSertificateEditModel> coursesInfo)
        {
            UpdateResult result = await this.coursesService.Update(
                cvId.ToString(),
                coursesInfo);

            return this.Ok(result);
        }
    }
}
