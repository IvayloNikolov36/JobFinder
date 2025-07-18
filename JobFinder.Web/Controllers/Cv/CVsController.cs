﻿using JobFinder.Services.Cv;
using JobFinder.Web.Infrastructure.Extensions;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.AdApplication;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv
{
    [Authorize]
    public class CVsController : ApiController
    {
        private readonly ICvsService cvsService;

        public CVsController(ICvsService cvsService)
        {
            this.cvsService = cvsService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<ActionResult<BasicViewModel>> Create([FromBody] CVCreateInputModel cvModel)
        {
            string userId = this.User.GetCurrentUserId();

            string cvId = await this.cvsService
                .CreateAsync(cvModel, userId, invalidateCache: true);

            object resultObject = new { cvId };

            return this.CreatedAtRoute("GetOwnCvData", resultObject, resultObject);
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<ActionResult<IEnumerable<CvListingModel>>> GetAllMine()
        {
            string userId = this.User.GetCurrentUserId();

            IEnumerable<CvListingModel> myCvs = await this.cvsService.All(userId);

            return this.Ok(myCvs);
        }

        [HttpGet]
        [Route("{cvId:guid}", Name = "GetOwnCvData")]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<ActionResult<CvDataViewModel>> GetOwnCvData([FromRoute] Guid cvId)
        {
            MyCvDataViewModel cv = await this.cvsService.GetOwnCvData(
                cvId.ToString(),
                this.User.GetCurrentUserId());

            return this.Ok(cv);
        }

        [HttpGet]
        [Route("preview/{CvId}/{JobAdId}")]
        [Authorize(Roles = CompanyRole)]
        [ServiceFilter(typeof(ValidateCompanyAccessingCVSentForOwnAd))]
        public async Task<ActionResult<CvDataViewModel>> GetCvPreview([FromRoute] ApplicationPreviewInfoInputModel model)
        {
            CvPreviewDataViewModel cv = await this.cvsService.GetUserCvData(model.CvId);

            return this.Ok(cv);
        }

        [HttpGet]
        [Route("preview/{cvRequestId:int}")]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> GetRequestedCv([FromRoute] int cvRequestId)
        {
            string userId = this.User.GetCurrentUserId();

            CvPreviewDataViewModel cvData = await this.cvsService
                .GetRequestedCvData(cvRequestId, userId);

            return this.Ok(cvData);
        }

        [HttpDelete]
        [Route("delete/{cvId}")]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> DeleteCv([FromRoute] string cvId)
        {
            await this.cvsService.Delete(cvId);

            return this.NoContent();
        }

        [HttpGet]
        [Route("generate-pdf/{cvId}")]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<ActionResult> GenerateCVPdf(string cvId)
        {
            byte[] cvPdf = await this.cvsService.GeneratePdf(cvId, this.User.GetCurrentUserId());

            return this.Ok(cvPdf);
        }
    }
}
