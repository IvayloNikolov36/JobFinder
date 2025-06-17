using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.Cv
{
    public interface ILanguageInfosService
    {
        Task<UpdateResult> Update(string cvId, IEnumerable<LanguageInfoEditModel> languagesInfo);
    }
}
