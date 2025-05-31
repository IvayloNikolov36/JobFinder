using JobFinder.Services.CV;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.CVModels;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers.CurriculumVitae
{
    public class PersonalDetailsController : BaseCVsController
    {
        private readonly IPersonalInfosService personalDetailsService;

        public PersonalDetailsController(IPersonalInfosService personalDetailsService)
        {
            this.personalDetailsService = personalDetailsService;
        }

        [HttpPut]
        [Route("{cvId}/update")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update([FromRoute] string cvId, [FromBody] PersonalInfoEditModel model)
        {
            await this.personalDetailsService.Update(model);

            return this.Ok();
        }
    }
}
