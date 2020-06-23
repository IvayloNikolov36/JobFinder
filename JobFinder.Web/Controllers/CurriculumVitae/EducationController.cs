namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    public class EducationController : ApiController
    {
        private readonly IEducationService educationservice;

        public EducationController(IEducationService educationservice)
        {
            this.educationservice = educationservice;
        }

        [HttpPost("create/{id}")]
        public async Task<ActionResult<int>> Create(string id, [FromBody] EducationInputModel model)
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

            int educationId = await this.educationservice.CreateAsync(
                model.CurriculumVitaeId, model.FromDate, model.ToDate, 
                model.Location,model.EducationLevel, model.Major, model.MainSubjects);

            return this.Ok(educationId);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Edit(string id, [FromBody] EducationEditModel model)
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

            bool isUpdated = await this.educationservice.UpdateAsync(
                model.EducationId, model.FromDate, model.ToDate,
                model.Location, model.EducationLevel, model.Major, model.MainSubjects);

            if (!isUpdated)
            {
                return this.BadRequest(new { Message = "Education details are not updated!" });
            }

            return this.Ok(new { Message = "Education successfully updated!" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(string id, [FromBody] EducationDeleteModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != id)
            {
                return this.BadRequest(new { Message = "User Id is incorrect!" });
            }

            bool isDeleted = await this.educationservice.DeleteAsync(model.EducationId);

            if (!isDeleted)
            {
                return this.BadRequest(new { Message = "Education is not deleted!" });
            }

            return this.Ok(new { Message = "Education successfully deleted!" });
        }
    }
}
