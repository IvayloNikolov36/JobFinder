using JobFinder.Services.Cv;
using JobFinder.Web.Infrastructure.Filters;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static JobFinder.Web.Infrastructure.WebConstants;

namespace JobFinder.Web.Controllers.Cv
{
    [Authorize]
    [Route("api/languages-info")]
    public class LanguagesInfoController : ApiController
    {
        private readonly ILanguagesInfoService languagesInfoService;

        public LanguagesInfoController(ILanguagesInfoService languageService)
        {
            this.languagesInfoService = languageService;
        }
      
        [HttpPut]
        [Route("{cvId:guid}/update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateResult<int>))]
        [Authorize(Roles = JobSeekerRole)]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<LanguageInfoEditModel> languagesInfo)
        {
            UpdateResult<int> result = await this.languagesInfoService.Update(
                cvId.ToString(),
                languagesInfo);

            return this.Ok(result);
        }
    }
}
