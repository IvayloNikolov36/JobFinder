namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Infrastructure.Filters;
    using JobFinder.Web.Models.CurriculumVitae;
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
        public async Task<ActionResult<IEnumerable<CourseCertificateListingModel>>> All([FromQuery] string cvId)
        {
            var courses = await this.coursesService.AllAsync<CourseCertificateListingModel>(cvId);

            return this.Ok(courses);
        }

        [HttpPost("{cvId:guid}")]
        [ServiceFilter(typeof(ValidateCvIdExistsServiceFilter))]
        public async Task<ActionResult<IEnumerable<int>>> Add(Guid cvId, [FromBody] CourseSertificateInputModel[] models)
        {
            IList<int> entitiesIds = new List<int>();
            foreach (var model in models)
            {
                var entityId = await this.coursesService
                    .AddAsync(cvId.ToString(), model.CourseName, model.CertificateUrl);

                entitiesIds.Add(entityId);
            }

            return this.Ok(entitiesIds);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] CourseSertificateEditModel model)
        {
            bool isUpdated = await this.coursesService.UpdateAsync(model.Id, model.CourseName, model.CertificateUrl);
            if (!isUpdated)
            {
                return this.BadRequest(new { Title = "Not updated!" });
            }

            return this.Ok(new { Message = "Entity successfully updated!" });
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
