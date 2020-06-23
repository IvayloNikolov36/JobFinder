namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class SkillsController : ApiController
    {
        private readonly ISkillsService skillsService;

        public SkillsController(ISkillsService skillsService)
        {
            this.skillsService = skillsService;
        }

        [HttpGet("")]
        public async Task<ActionResult<SkillsViewModel>> Get([FromQuery] int skillsId)
        {
            var skills = await this.skillsService.GetAsync<SkillsViewModel>(skillsId);

            return skills;
        }

        [HttpPost("add")]
        public async Task<ActionResult<int>> Add([FromBody] SkillsInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Errors = this.ModelState.Values });
            }

            int skillsId = await this.skillsService.AddAsync(model.CurriculumVitaeId, model.ComputerSkills, 
                model.Skills, model.HasManagedPeople, model.HasDrivingLicense);

            return this.Ok(skillsId);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Edit([FromBody] SkillsEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Errors = this.ModelState.Values });
            }

            bool isUpdated = await this.skillsService.UpdateAsync(model.SkillId, model.ComputerSkills, model.Skills, 
                model.HasManagedPeople, model.HasDrivingLicense);

            if (!isUpdated)
            {
                return this.BadRequest(new { Message = "Skills are not updated!" });
            }

            return this.Ok(new { Message = "Skills successfully updated!" });
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromQuery] int id)
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
