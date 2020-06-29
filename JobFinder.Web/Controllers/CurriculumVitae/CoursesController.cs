namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CoursesController : BaseCVsController
    {
        private readonly ICoursesSertificatesService coursesService;

        public CoursesController(ICoursesSertificatesService coursesService)
        {
            this.coursesService = coursesService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CoursesSertificatesListingModel>>> All([FromQuery] string cvId)
        {
            var courses = await this.coursesService.AllAsync<CoursesSertificatesListingModel>(cvId);

            return this.Ok(courses);
        } 

        [HttpPost("create")]
        public async Task<ActionResult<int>> Add([FromBody] CourseSertificateInputModel model)
        {
            var entityId = await this.coursesService
                .AddAsync(model.CvId, model.CourseName, model.CertificateUrl);

            return this.Ok(entityId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Edit([FromBody] CourseSertificateEditModel model)
        {
            bool isUpdated = await this.coursesService.UpdateAsync(model.Id, model.CourseName, model.CertificateUrl);
            if (!isUpdated)
            {
                return this.BadRequest(new { Message = "Not updated!" });
            }

            return this.Ok(new { Message = "Entity successfully updated!" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isDeleted = await this.coursesService.DeleteAsync(id);

            if (!isDeleted)
            {
                return this.BadRequest(new { Message = "Entity not deleted!" });
            }

            return this.Ok(new { Message = "Entity successfully deleted!" });
        }
    }
}
