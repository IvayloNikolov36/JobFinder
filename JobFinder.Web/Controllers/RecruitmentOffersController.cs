using JobFinder.Data.Models;
using JobFinder.Services;
using JobFinder.Web.Models.RecruitmentOffers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobFinder.Web.Controllers
{
    public class RecruitmentOffersController : ApiController
    {
        private readonly IRecruitmentOfferService offersService;

        public RecruitmentOffersController(IRecruitmentOfferService offersService)
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
                return this.NotFound(new { Message = "Offer not found!" });
            }

            return offer;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Create(RecruitmentOfferBindingModel model)
        {
            DateTime expirationDate = DateTime.UtcNow.AddDays(model.DaysActive);

            await this.offersService
                .CreateAsync(model.PublisherId, model.Position, model.Desription, expirationDate);

            return this.Ok(new { Message = "Successfully created a recruitment offer!" });
        }

        [HttpPut("edit/{id}")]
        [Authorize]
        public async Task<ActionResult> Edit(int id, [FromBody] RecruitmentOfferEditModel model)
        {
            var offerFromDb = await this.offersService.GetAsync(id);

            if (offerFromDb == null)
            {
                return this.NotFound(new { Message = "No such a recruitment offer found!" });
            }

            //TODO: make filters!
            //TODO: think about for editing expiration

            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != offerFromDb.PublisherId)
            {
                return this.BadRequest(new { Message = "You can't edit other companies offers!" });
            }

            await this.offersService
                .EditAsync(id, model.Position, model.Desription, model.DaysActive);

            return this.Ok(new { Message = "Offer successfully updated!" });
        }
    }
}
