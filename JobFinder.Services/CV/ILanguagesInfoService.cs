using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.Cv
{
    public interface ILanguagesInfoService
    {
        Task<UpdateResult> Update(string cvId, IEnumerable<LanguageInfoEditModel> languagesInfo);
    }
}
