using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Mvc;

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
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update([FromRoute] string cvId, [FromBody] SkillsEditModel model)
        {
            await this.skillsService.Update(model);

            return this.Ok();
        }
    }
}
