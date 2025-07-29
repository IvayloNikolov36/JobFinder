using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv
{
    [Authorize]
    [Route("api/education-info")]
    public class EducationsController : ApiController
    {
        private readonly IEducationsInfoService educationService;

        public EducationsController(IEducationsInfoService educationService)
        {
            this.educationService = educationService;
        }

        [HttpPut]
        [Route("{cvId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateResult))]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<EducationEditModel> educationsModel)
        {
            UpdateResult result = await this.educationService.Update(
                cvId.ToString(),
                educationsModel);

            return this.Ok(result);
        }
    }
}
