using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv
{
    public class SkillsController : BaseCVsController
    {
        private readonly ISkillsInfosService skillsService;

        public SkillsController(ISkillsInfosService skillsService)
        {
            this.skillsService = skillsService;
        }

        [HttpPut]
        [Route("{cvId}/update")]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update([FromRoute] string cvId, [FromBody] SkillsEditModel model)
        {
            await this.skillsService.Update(cvId, model);

            return this.Ok();
        }
    }
}
