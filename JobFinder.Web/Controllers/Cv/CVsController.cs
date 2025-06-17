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
        [Route("create")]
        public async Task<ActionResult<BasicViewModel>> Create([FromBody] CVCreateInputModel cvModel)
        {
            string userId = this.User.GetCurrentUserId();

            string cvId = await this.cvsService.CreateAsync(cvModel, userId);

            object resultObject = new { cvId };

            return this.CreatedAtRoute("GetOwnCvData", resultObject, resultObject);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<CvListingModel>>> GetAllMine()
        {
            string userId = this.User.GetCurrentUserId();

            IEnumerable<CvListingModel> myCvs = await this.cvsService.All(userId);

            return this.Ok(myCvs);
        }

        [HttpGet]
        [Route("{cvId:guid}", Name = "GetOwnCvData")]
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

        [HttpDelete]
        [Route("delete/{id}")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> DeleteCv([FromRoute] string id)
        {
            await this.cvsService.Delete(id);

            return this.NoContent();
        }

        [HttpGet]
        [Route("generate-pdf/{id}")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<ActionResult> GenerateCVPdf(string id)
        {
            byte[] cvPdf = await this.cvsService.GeneratePdf(id, this.User.GetCurrentUserId());

            return this.Ok(cvPdf);
        }
    }
}
