namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CV;
    using JobFinder.Web.Infrastructure.Filters;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class PersonalDetailsController : BaseCVsController
    {
        private readonly IPersonalInfosService personalDetailsService;

        public PersonalDetailsController(IPersonalInfosService personalDetailsService)
        {
            this.personalDetailsService = personalDetailsService;
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
