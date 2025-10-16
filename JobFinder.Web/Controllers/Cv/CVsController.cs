using JobFinder.Services.Cv;
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IdentityViewModel<string>))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<ActionResult<BasicViewModel>> Create(
            [FromBody] CVCreateInputModel cvModel)
        {
            string userId = this.User.GetCurrentUserId();

            IdentityViewModel<string> result = await this.cvsService
                .CreateAsync(cvModel, userId, invalidateCache: true);

            object routeValue = new { cvId = result.Id };

            return this.CreatedAtRoute("GetOwnCvData", routeValue, routeValue);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<CvListingModel>))]
        [Authorize(Roles = JobSeekerRole)]
        public async Task<ActionResult<IEnumerable<CvListingModel>>> GetAllMine()
        {
            string userId = this.User.GetCurrentUserId();

            IEnumerable<CvListingModel> myCvs = await this.cvsService.All(userId);

            return this.Ok(myCvs);
        }

        [HttpGet]
        [Route("{cvId:guid}", Name = "GetOwnCvData")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyCvDataViewModel))]
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
        [Route("{CvId}/{JobAdId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CvPreviewDataViewModel))]
        [Authorize(Roles = CompanyRole)]
        [ServiceFilter(typeof(ValidateCompanyAccessingCVSentForOwnAd))]
        public async Task<ActionResult<CvDataViewModel>> GetCvPreview(
            [FromRoute] ApplicationPreviewInfoInputModel model)
        {
            CvPreviewDataViewModel cv = await this.cvsService
                .GetUserCvData(model.CvId);

            return this.Ok(cv);
        }

        [HttpGet]
        [Route("preview/{cvRequestId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CvPreviewDataViewModel))]
        [Authorize(Roles = CompanyRole)]
        public async Task<IActionResult> GetRequestedCv([FromRoute] int cvRequestId)
        {
            string userId = this.User.GetCurrentUserId();

            CvPreviewDataViewModel cvData = await this.cvsService
                .GetRequestedCvData(cvRequestId, userId);

            return this.Ok(cvData);
        }

        [HttpDelete]
        [Route("{cvId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> DeleteCv([FromRoute] Guid cvId)
        {
            await this.cvsService.Delete(
                cvId.ToString(),
                this.User.GetCurrentUserId());

            return this.NoContent();
        }

        [HttpGet]
        [Route("generate-pdf/{cvId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(byte[]))]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<ActionResult> GenerateCVPdf(Guid cvId)
        {
            byte[] cvPdf = await this.cvsService.GeneratePdf(
                cvId.ToString(),
                this.User.GetCurrentUserId());

            return this.Ok(cvPdf);
        }
    }
}
