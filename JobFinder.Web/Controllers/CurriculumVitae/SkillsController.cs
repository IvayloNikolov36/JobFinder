namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CV;
    using JobFinder.Web.Infrastructure.Filters;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
            bool isUpdated = await this.skillsService.UpdateAsync(model);

            if (!isUpdated)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}
