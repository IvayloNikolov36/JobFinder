namespace JobFinder.Services.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILanguageInfoService
    {
        Task<IEnumerable<T>> AllAsync<T>(string cvId);

        Task<int> AddAsync(string cvId, LanguageType languageType, 
            LanguageLevel comprehension, LanguageLevel speaking, LanguageLevel writing);

        Task<bool> UpdateAsync(int languageInfoId, LanguageType languageType,
            LanguageLevel comprehension, LanguageLevel speaking, LanguageLevel writing);

        Task<bool> DeleteAsync(int languageInfoId);
    }
}
