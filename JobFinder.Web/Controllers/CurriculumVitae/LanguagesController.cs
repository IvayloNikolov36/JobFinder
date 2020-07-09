namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CurriculumVitae;
    using Microsoft.AspNetCore.Mvc;
    using System;
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

        [HttpGet("types")]
        public IActionResult GetLanguageTypes()
        {
            var types = new List<EnumTypeViewModel>();
            foreach (var languageType in Enum.GetValues(typeof(LanguageType)))
            {
                types.Add(new EnumTypeViewModel((int)languageType, languageType.ToString()));
            }

            return this.Ok(types);
        }

        [HttpGet("levels")]
        public IActionResult GetLanguageLevel()
        {
            var educationLevels = new List<EnumTypeViewModel>();
            foreach (var level in Enum.GetValues(typeof(LanguageLevel)))
            {
                educationLevels.Add(new EnumTypeViewModel((int)level, level.ToString()));
            }

            return this.Ok(educationLevels);
        }
    }
}
