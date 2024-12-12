namespace JobFinder.Services.CV
{
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CVModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILanguageInfosService
    {
        Task<UpdateResult> UpdateAsync(string cvId, IEnumerable<LanguageInfoEditModel> languagesInfo);
    }
}
