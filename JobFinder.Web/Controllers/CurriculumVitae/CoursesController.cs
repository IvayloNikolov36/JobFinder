namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CoursesController : BaseCVsController
    {
        private readonly ICoursesSertificatesService coursesService;

        public CoursesController(ICoursesSertificatesService coursesService)
        {
            this.coursesService = coursesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseCertificateViewModel>>> All([FromQuery] string cvId)
        {
            IEnumerable<CourseCertificateViewModel> courses = await this.coursesService.AllAsync<CourseCertificateViewModel>(cvId);

            return this.Ok(courses);
        }

        [HttpPut("{cvId:guid}/update")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<CourseSertificateEditModel> coursesInfo)
        {
            await this.coursesService.UpdateAsync(cvId.ToString(), coursesInfo);

            return this.Ok(new { Message = "Courses info successfully updated!" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isDeleted = await this.coursesService.DeleteAsync(id);

            if (!isDeleted)
            {
                return this.BadRequest(new { Title = "Entity not deleted!" });
            }

            return this.Ok(new { Message = "Entity successfully deleted!" });
        }
    }
}
