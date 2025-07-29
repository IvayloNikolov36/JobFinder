using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv
{
    [Authorize]
    [Route("api/personal-info")]
    public class PersonalInfoController : ApiController
    {
        private readonly IPersonalInfoService personalInfoService;

        public PersonalInfoController(IPersonalInfoService personalInfoService)
        {
            this.personalInfoService = personalInfoService;
        }

        [HttpPut]
        [Route("{cvId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] PersonalInfoEditModel model)
        {
            await this.personalInfoService.Update(
                cvId.ToString(),
                model);

            return this.Ok();
        }
    }
}
