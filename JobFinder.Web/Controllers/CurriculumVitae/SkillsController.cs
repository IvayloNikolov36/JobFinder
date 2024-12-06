namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CV;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SkillsController : BaseCVsController
    {
        private readonly ISkillsService skillsService;

        public SkillsController(ISkillsService skillsService)
        {
            this.skillsService = skillsService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SkillsViewModel>> Get(int id)
        {
            var skills = await this.skillsService.GetAsync<SkillsViewModel>(id);

            return skills;
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] SkillsEditModel model)
        {
            bool isUpdated = await this.skillsService.UpdateAsync(model);

            if (!isUpdated)
            {
                return this.NotFound();
            }

            return this.Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await this.skillsService.DeleteAsync(id);

            // TODO: check such statements and with NotFound
            if (!isDeleted)
            {
                return this.BadRequest(new { Title = "Skills are not deleted!" });
            }

            return this.Ok(new { Message = "Skills successfully deleted!" });
        }

        [HttpGet("driving-categories")]
        public async Task<ActionResult<IEnumerable<object>>> GetDrivingCategories()
        {
            var categories =  await this.skillsService.GetDrivingCategories<SkillsDrivingCategoryViewModel>();

            return this.Ok(categories);
        }
    }
}
