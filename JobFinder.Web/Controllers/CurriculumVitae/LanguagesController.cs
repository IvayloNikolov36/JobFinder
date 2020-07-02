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

        [HttpPost("create/{id}")]
        public async Task<ActionResult<IEnumerable<int>>> Add(string id, [FromBody] LanguageInfoInputModel[] models)
        {
            IList<int> entitiesIds = new List<int>();
            foreach (LanguageInfoInputModel model in models)
            {
                int languageInfoId = await this.languageService.AddAsync(id,
                model.LanguageType, model.Comprehension, model.Speaking, model.Writing);

                entitiesIds.Add(languageInfoId);
            }
            
            return this.Ok(entitiesIds);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Edit([FromBody] LanguageInfoEditModel model)
        {
            bool isUpdated = await this.languageService.UpdateAsync(model.LanguageInfoId,
                 model.LanguageType, model.Comprehension, model.Speaking, model.Writing);

            if (!isUpdated)
            {
                return this.BadRequest(new { Title = "Language info not updated!" });
            }

            return this.Ok(new { Message = "Language info successfully updated!" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
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
