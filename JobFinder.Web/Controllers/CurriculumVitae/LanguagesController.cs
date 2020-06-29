namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LanguagesController : BaseCVsController
    {
        private readonly ILanguageInfoService languageService;

        public LanguagesController(ILanguageInfoService languageService)
        {
            this.languageService = languageService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<LanguagesListingModel>>> All([FromQuery] string cvId)
        {
            var languagesInfo = await this.languageService.AllAsync<LanguagesListingModel>(cvId);

            return this.Ok(languagesInfo);
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> Add([FromBody] LanguageInfoInputModel model)
        {
            int languageInfoId = await this.languageService.AddAsync(model.CvId,
                model.LanguageType, model.Comprehension, model.Speaking, model.Writing);

            return this.Ok(languageInfoId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Edit([FromBody] LanguageInfoEditModel model)
        {
            bool isUpdated = await this.languageService.UpdateAsync(model.LanguageInfoId,
                 model.LanguageType, model.Comprehension, model.Speaking, model.Writing);

            if (!isUpdated)
            {
                return this.BadRequest(new { Message = "Language info not updated!" });
            }

            return this.Ok(new { Message = "Language info successfully updated!" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            bool isDeleted = await this.languageService.DeleteAsync(id);

            if (!isDeleted)
            {
                return this.BadRequest(new { Message = "Language info is not deleted!" });
            }

            return this.Ok(new { Message = "Language info successfully deleted!" });
        }
    }
}
