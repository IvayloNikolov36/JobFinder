namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CV;
    using JobFinder.Web.Infrastructure.Filters;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LanguagesInfoController : BaseCVsController
    {
        private readonly ILanguageInfosService languagesInfosService;

        public LanguagesInfoController(ILanguageInfosService languageService)
        {
            this.languagesInfosService = languageService;
        }
      
        [HttpPut]
        [Route("{cvId:guid}/update")]
        [ServiceFilter(typeof(ValidateCvIdBelongsToUser))]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<LanguageInfoEditModel> languagesInfo)
        {
            UpdateResult result = await this.languagesInfosService.UpdateAsync(cvId.ToString(), languagesInfo);

            return this.Ok(result);
        }
    }
}
