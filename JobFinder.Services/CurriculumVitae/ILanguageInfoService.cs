namespace JobFinder.Services.CurriculumVitae
{
    using JobFinder.Web.Models.CVModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILanguageInfoService
    {
        Task<IEnumerable<T>> AllAsync<T>(string cvId);

        Task UpdateAsync(IEnumerable<LanguageInfoEditModel> languagesInfo);

        Task<bool> DeleteAsync(int languageInfoId);
    }
}
