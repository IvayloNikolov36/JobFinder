namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize]
    public class LanguagesController : ApiController
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

        [HttpPost("add")]
        public async Task<ActionResult<int>> Add([FromBody] LanguageInfoInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Errors = this.ModelState.Values });
            }

            int languageInfoId = await this.languageService.AddAsync(model.CurriculumVitaeId,
                model.LanguageType, model.Comprehension, model.Speaking, model.Writing);

            return this.Ok(languageInfoId);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Edit([FromBody] LanguageInfoEditModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(new { Errors = this.ModelState.Values });
            }

            bool isUpdated = await this.languageService.UpdateAsync(model.LanguageInfoId,
                 model.LanguageType, model.Comprehension, model.Speaking, model.Writing);

            if (!isUpdated)
            {
                return this.BadRequest(new { Message = "Language info not updated!" });
            }

            return this.Ok(new { Message = "Language info successfully updated!" });
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromQuery] int id)
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
