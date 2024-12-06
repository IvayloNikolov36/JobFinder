namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LanguagesInfoController : BaseCVsController
    {
        private readonly ILanguageInfoService languageService;

        public LanguagesInfoController(ILanguageInfoService languageService)
        {
            this.languageService = languageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageInfoViewModel>>> All([FromQuery] string cvId)
        {
            IEnumerable<LanguageInfoViewModel> languagesInfo = await this.languageService
                .AllAsync<LanguageInfoViewModel>(cvId);

            return this.Ok(languagesInfo);
        }
      
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] IEnumerable<LanguageInfoEditModel> languagesInfo)
        {
            await this.languageService.UpdateAsync(languagesInfo);

            return this.Ok(new { Message = "Languages info successfully updated!" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await this.languageService.DeleteAsync(id);

            if (!isDeleted)
            {
                return this.BadRequest(new { Title = "Language info is not deleted!" });
            }

            return this.Ok(new { Message = "Language info successfully deleted!" });
        }
    }
}
