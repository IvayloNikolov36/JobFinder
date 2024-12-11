namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CV;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LanguagesInfoController : BaseCVsController
    {
        private readonly ILanguageInfoService languagesInfosService;

        public LanguagesInfoController(ILanguageInfoService languageService)
        {
            this.languagesInfosService = languageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageInfoViewModel>>> All([FromQuery] string cvId)
        {
            IEnumerable<LanguageInfoViewModel> languagesInfo = await this.languagesInfosService
                .AllAsync<LanguageInfoViewModel>(cvId);

            return this.Ok(languagesInfo);
        }
      
        [HttpPut]
        [Route("{cvId:guid}/update")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<LanguageInfoEditModel> languagesInfo)
        {
            UpdateResult result = await this.languagesInfosService.UpdateAsync(cvId.ToString(), languagesInfo);

            return this.Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await this.languagesInfosService.DeleteAsync(id);

            if (!isDeleted)
            {
                return this.BadRequest(new { Title = "Language info is not deleted!" });
            }

            return this.Ok(new { Message = "Language info successfully deleted!" });
        }
    }
}
