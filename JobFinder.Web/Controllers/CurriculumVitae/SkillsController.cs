namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
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
        public async Task<ActionResult<SkillsViewModel>> Get(int id)
        {
            var skills = await this.skillsService.GetAsync<SkillsViewModel>(id);

            return skills;
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> Add([FromBody] SkillsInputModel model)
        {
            int skillsId = await this.skillsService.AddAsync(model.CvId, model.ComputerSkills, 
                model.Skills, model.HasManagedPeople, model.HasDrivingLicense);

            return this.Ok(skillsId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Edit([FromBody] SkillsEditModel model)
        {
            bool isUpdated = await this.skillsService.UpdateAsync(model.SkillId, model.ComputerSkills, model.Skills, 
                model.HasManagedPeople, model.HasDrivingLicense);

            if (!isUpdated)
            {
                return this.BadRequest(new { Message = "Skills are not updated!" });
            }

            return this.Ok(new { Message = "Skills successfully updated!" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isDeleted = await this.skillsService.DeleteAsync(id);

            if (!isDeleted)
            {
                return this.BadRequest(new { Message = "Skills are not deleted!" });
            }

            return this.Ok(new { Message = "Skills successfully deleted!" });
        }
    }
}
