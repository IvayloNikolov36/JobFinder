namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CV;
    using JobFinder.Web.Infrastructure.Filters;
    using JobFinder.Web.Models.Common;
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

        [HttpPut]
        [Route("{cvId:guid}/update")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<CourseSertificateEditModel> coursesInfo)
        {
            UpdateResult result = await this.coursesService.UpdateAsync(cvId.ToString(), coursesInfo);

            return this.Ok(result);
        }
    }
}
