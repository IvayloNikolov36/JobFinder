namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CV;
    using JobFinder.Web.Infrastructure.Filters;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class PersonalDetailsController : BaseCVsController
    {
        private readonly IPersonalDetailsService personalDetailsService;

        public PersonalDetailsController(IPersonalDetailsService personalDetailsService)
        {
            this.personalDetailsService = personalDetailsService;
        }

        [HttpGet]
        public async Task<ActionResult<PersonalInfoViewModel>> Get([FromQuery] string cvId)
        {
            PersonalInfoViewModel personalDetails = await this.personalDetailsService
                .GetAsync<PersonalInfoViewModel>(cvId);

            return this.Ok(personalDetails);
        }

        [HttpPut]
        [Route("update")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update([FromBody] PersonalDetailsEditModel model)
        {
            bool isUpdated = await this.personalDetailsService.UpdateAsync(model);

            if (!isUpdated)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}
