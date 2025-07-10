using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv
{
    public class PersonalInfoController : BaseCVsController
    {
        private readonly IPersonalInfosService personalInfoService;

        public PersonalInfoController(IPersonalInfosService personalInfoService)
        {
            this.personalInfoService = personalInfoService;
        }

        [HttpPut]
        [Route("{cvId}/update")]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update([FromRoute] string cvId, [FromBody] PersonalInfoEditModel model)
        {
            await this.personalInfoService.Update(cvId, model);

            return this.Ok();
        }
    }
}
