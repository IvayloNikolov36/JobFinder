namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    public class WorkExperienceController : ApiController
    {
        private readonly IWorkExperienceSerive workExperienceService;

        public WorkExperienceController(IWorkExperienceSerive workExperienceService)
        {
            this.workExperienceService = workExperienceService;
        }

        [HttpPost("create/{id}")]
        public async Task<ActionResult<int>> Create(string id, [FromBody] WorkExperienceInputModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != id)
            {
                return this.BadRequest(new { Message = "User Id is incorrect!" });
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Errors = this.ModelState.Values });
            }

            int workExperienceId = await this.workExperienceService.CreateAsync(model.FromDate, model.ToDate, model.JobTitle, 
                model.Organization, model.BusinessSector, model.Location, model.AditionalDetails);

            return this.Ok(workExperienceId);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] WorkExperienceEditModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != id)
            {
                return this.BadRequest(new { Message = "User Id is incorrect!" });
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Errors = this.ModelState.Values });
            }

            bool isUpdated = await this.workExperienceService.UpdateAsync(model.WorkExperienceId, model.FromDate, model.ToDate, model.JobTitle,
                model.Organization, model.BusinessSector, model.Location, model.AditionalDetails);

            if (!isUpdated)
            {
                return this.BadRequest(new { Message = "Work Experience not updated!" });
            }

            return this.Ok(new { Message = "Work Experience updated successfully!" } );
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(string id, [FromBody] WorkExperienceDeleteModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != id)
            {
                return this.BadRequest(new { Message = "User Id is incorrect!" });
            }

            bool isDeleted = await this.workExperienceService.DeleteAsync(model.WorkExperienceId);
            if (!isDeleted)
            {
                return this.BadRequest(new { Message = "Work Experience not deleted!" });
            }

            return this.Ok(new { Message = "Work Experience successfully deleted!" });
        }
    }
}
