using JobFinder.Data.Models;
using JobFinder.Services;
using JobFinder.Web.Models.JobAds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers
{
    public class JobAdsController : ApiController
    {
        private readonly IJobAdsService offersService;

        public JobAdsController(IJobAdsService offersService)
        {
            this.offersService = offersService;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RecruitmentOffer>>> All()
        {
            var offers = await this.offersService.AllAsync();

            return offers.ToList();
        }

        [HttpGet("details/{id}")]
        [Authorize]
        public async Task<ActionResult<RecruitmentOffer>> Get(int id)
        {
            var offer = await this.offersService.GetAsync(id);

            if (offer == null)
            {
                return this.NotFound(new { Message = NoAdFound });
            }

            return offer;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Create([FromBody] JobAdBindingModel model)
        {
            DateTime expirationDate = DateTime.UtcNow.AddDays(model.DaysActive);

            string publisherId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.offersService
                .CreateAsync(publisherId, model.Position, model.Description, expirationDate);

            return this.Ok(new { Message = SuccessOnCreation });
        }

        [HttpPut("edit/{id}")]
        [Authorize]
        public async Task<ActionResult> Edit(int id, [FromBody] JobAdEditModel model)
        {
            var offerFromDb = await this.offersService.GetAsync(id);

            if (offerFromDb == null)
            {
                return this.NotFound(new { Message = NoAdFound });
            }

            //TODO: make filters!
            //TODO: think about for editing expiration

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != offerFromDb.PublisherId)
            {
                return this.BadRequest(new { Message = CantEditAd });
            }

            await this.offersService
                .EditAsync(id, model.Position, model.Desription, model.DaysActive);

            return this.Ok(new { Message = UpdatedAd });
        }
    }
}
