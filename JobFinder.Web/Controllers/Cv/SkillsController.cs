using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv
{
    [Authorize]
    [Route("api/skills-info")]
    public class SkillsController : ApiController
    {
        private readonly ISkillsInfosService skillsService;

        public SkillsController(ISkillsInfosService skillsService)
        {
            this.skillsService = skillsService;
        }

        [HttpPut]
        [Route("{cvId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] SkillsEditModel model)
        {
            await this.skillsService.Update(
                cvId.ToString(),
                model);

            return this.Ok();
        }
    }
}
