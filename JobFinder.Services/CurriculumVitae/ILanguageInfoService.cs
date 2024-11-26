namespace JobFinder.Services.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using JobFinder.Web.Models.CVModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILanguageInfoService
    {
        Task<IEnumerable<T>> AllAsync<T>(string cvId);

        Task<int> AddAsync(string cvId, LanguageType languageType, 
            LanguageLevel comprehension, LanguageLevel speaking, LanguageLevel writing);

        Task UpdateAsync(string cvId, IEnumerable<LanguageInfoEditModel> languagesInfo);

        Task<bool> DeleteAsync(int languageInfoId);
    }
}
