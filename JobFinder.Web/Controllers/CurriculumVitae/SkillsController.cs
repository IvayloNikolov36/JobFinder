namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CV;
    using JobFinder.Web.Infrastructure.Filters;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class SkillsController : BaseCVsController
    {
        private readonly ISkillsService skillsService;

        public SkillsController(ISkillsService skillsService)
        {
            this.skillsService = skillsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillsViewModel>> Get([FromRoute] int id)
        {
            SkillsViewModel skillsInfo = await this.skillsService.GetAsync<SkillsViewModel>(id);

            return this.Ok(skillsInfo);
        }

        [HttpPut]
        [Route("update")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update([FromBody] SkillsEditModel model)
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
