namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
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

        [HttpPost]
        public async Task<ActionResult<int>> Add([FromBody] SkillsInputModel model)
        {
            int skillsId = await this.skillsService.AddAsync(model.CvId, model.ComputerSkills, 
                model.Skills, model.HasManagedPeople, model.HasDrivingLicense);

            return this.Ok(skillsId);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SkillsEditModel model)
        {
            bool isUpdated = await this.skillsService.UpdateAsync(id, model.ComputerSkills, model.Skills, 
                model.HasManagedPeople, model.HasDrivingLicense);

            if (!isUpdated)
            {
                return this.BadRequest(new { Title = "Skills are not updated!" });
            }

            return this.Ok(new { Message = "Skills successfully updated!" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await this.skillsService.DeleteAsync(id);

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
