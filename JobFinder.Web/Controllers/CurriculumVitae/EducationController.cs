namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    public class EducationController : ApiController
    {
        private readonly IEducationService educationService;

        public EducationController(IEducationService educationService)
        {
            this.educationService = educationService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<EducationsListingModel>> GetAll([FromQuery] string cvId)
        {
            var educations = await this.educationService.AllAsync<EducationsListingModel>(cvId);

            return this.Ok(educations);
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> Create([FromBody] EducationInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Errors = this.ModelState.Values });
            }

            int educationId = await this.educationService.CreateAsync(
                model.CurriculumVitaeId, model.FromDate, model.ToDate, 
                model.Location,model.EducationLevel, model.Major, model.MainSubjects);

            return this.Ok(educationId);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Edit([FromBody] EducationEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Errors = this.ModelState.Values });
            }

            bool isUpdated = await this.educationService.UpdateAsync(
                model.EducationId, model.FromDate, model.ToDate,
                model.Location, model.EducationLevel, model.Major, model.MainSubjects);

            if (!isUpdated)
            {
                return this.BadRequest(new { Message = "Education details are not updated!" });
            }

            return this.Ok(new { Message = "Education successfully updated!" });
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            bool isDeleted = await this.educationService.DeleteAsync(id);

            if (!isDeleted)
            {
                return this.BadRequest(new { Message = "Education is not deleted!" });
            }

            return this.Ok(new { Message = "Education successfully deleted!" });
        }
    }
}
