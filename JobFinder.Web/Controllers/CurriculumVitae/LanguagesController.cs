﻿namespace JobFinder.Web.Controllers.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Web.Infrastructure.Filters;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CVModels;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageInfoListingModel>>> All([FromQuery] string cvId)
        {
            var languagesInfo = await this.languageService.AllAsync<LanguageInfoListingModel>(cvId);

            return this.Ok(languagesInfo);
        }

        [HttpPost("{cvId:guid}")]
        [ServiceFilter(typeof(ValidateCvIdExistsServiceFilter))]
        public async Task<ActionResult<IEnumerable<int>>> Add(Guid cvId, [FromBody] LanguageInfoInputModel[] models)
        {
            IList<int> entitiesIds = new List<int>();
            foreach (LanguageInfoInputModel model in models)
            {
                int languageInfoId = await this.languageService.AddAsync(cvId.ToString(),
                model.LanguageType, model.Comprehension, model.Speaking, model.Writing);

                entitiesIds.Add(languageInfoId);
            }
            
            return this.Ok(entitiesIds);
        }

        [HttpPut("{cvId:guid}/update")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid cvId,
            [FromBody] IEnumerable<LanguageInfoEditModel> languagesInfo)
        {
            await this.languageService.UpdateAsync(cvId.ToString(), languagesInfo);

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
