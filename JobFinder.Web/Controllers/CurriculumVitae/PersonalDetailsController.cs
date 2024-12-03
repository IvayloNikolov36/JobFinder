namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CVModels;
    using JobFinder.Web.Models.Common;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PersonalDetailsController : BaseCVsController
    {
        private readonly IPersonalDetailsService personalDetailsService;

        public PersonalDetailsController(IPersonalDetailsService personalDetailsService)
        {
            this.personalDetailsService = personalDetailsService;
        }

        [HttpGet]
        public async Task<ActionResult<PersonalDetailsViewModel>> Get([FromQuery] string cvId)
        {
            var personalDetails = await this.personalDetailsService
                .GetAsync<PersonalDetailsViewModel>(cvId);

            return this.Ok(personalDetails);
        }

        //[HttpPost]
        //[Route("{cvId:guid}/create")]
        //public async Task<ActionResult<int>> Create([FromRoute] Guid cvId, [FromBody] PersonalDetailsInputModel model)
        //{
        //    int objectId = await this.personalDetailsService.CreateAsync(
        //        cvId.ToString(),
        //        model.FirstName,
        //        model.MiddleName,
        //        model.LastName,
        //        model.Birthdate,
        //        model.Gender,
        //        model.Email,
        //        model.Phone,
        //        model.Country,
        //        model.CitizenShip,
        //        model.City);

        //    return this.Ok(objectId);
        //}

        [HttpPut("{cvId:guid}/update")]
        public async Task<IActionResult> Edit([FromRoute] Guid cvId, [FromBody] PersonalDetailsEditModel model)
        {
            bool isUpdated = await this.personalDetailsService.UpdateAsync(cvId.ToString(), model);

            if (!isUpdated)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}
