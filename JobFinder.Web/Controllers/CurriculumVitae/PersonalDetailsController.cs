namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class PersonalDetailsController : BaseCVsController
    {
        private readonly IPersonalDetailsService personalDetailsService;

        public PersonalDetailsController(IPersonalDetailsService personalDetailsService)
        {
            this.personalDetailsService = personalDetailsService;
        }

        [HttpGet("")]
        public async Task<ActionResult<PersonalDetailsViewModel>> Get([FromQuery] string cvId)
        {
            var personalDetails = await this.personalDetailsService
                .GetAsync<PersonalDetailsViewModel>(cvId);

            return this.Ok(personalDetails);
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> Create([FromBody] PersonalDetailsnputModel model)
        {
            //TODO: make filter
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Errors = this.ModelState.Values });
            }

            int objectId = await this.personalDetailsService.CreateAsync(
                model.CvId,
                model.FirstName,
                model.MiddleName,
                model.LastName,
                model.Birthdate,
                model.Gender,
                model.Email,
                model.Phone,
                model.Country,
                model.CitizenShip,
                model.City);

            return this.Ok(objectId);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(PersonalDetailsEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Errors = this.ModelState.Values });
            }

            bool isUpdated = await this.personalDetailsService.UpdateAsync(
                model.PersonalDetailsId,
                model.FirstName,
                model.MiddleName,
                model.LastName,
                model.Birthdate,
                model.Gender,
                model.Email,
                model.Phone,
                model.Country,
                model.CitizenShip,
                model.City);

            if (!isUpdated)
            {
                return this.BadRequest(new { Message = "Invalid personal details id!" });
            }

            return this.Ok(new { Message = "Personal details updated successfully!" });
        }

    }
}
