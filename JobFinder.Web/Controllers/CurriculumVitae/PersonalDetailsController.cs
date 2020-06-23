namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    public class PersonalDetailsController : ApiController
    {
        private readonly IPersonalDetailsService personalDetailsService;

        public PersonalDetailsController(IPersonalDetailsService personalDetailsService)
        {
            this.personalDetailsService = personalDetailsService;
        }

        [HttpPost("create/{id}")]
        public async Task<ActionResult<int>> Create(string id, [FromBody] PersonalDetailsnputModel model)
        {
            //TODO: make filter
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != id)
            {
                return this.BadRequest(new { Message = "User Id is incorrect!" });
            }

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

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(string id, PersonalDetailsEditModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != id)
            {
                return this.BadRequest(new { Message = "User Id is incorrect!" });
            }

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
