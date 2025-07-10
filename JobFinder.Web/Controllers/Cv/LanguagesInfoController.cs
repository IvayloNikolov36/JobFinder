using JobFinder.Services.Cv;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv
{
    public class LanguagesInfoController : BaseCVsController
    {
        private readonly ILanguageInfosService languagesInfosService;

        public LanguagesInfoController(ILanguageInfosService languageService)
        {
            this.languagesInfosService = languageService;
        }
      
        [HttpPut]
        [Route("{cvId:guid}/update")]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<LanguageInfoEditModel> languagesInfo)
        {
            UpdateResult result = await this.languagesInfosService.Update(cvId.ToString(), languagesInfo);

            return this.Ok(result);
        }
    }
}
