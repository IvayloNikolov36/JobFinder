using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.CV
{
    public interface IEducationsInfoService
    {
        Task<UpdateResult> Update(string cvId, IEnumerable<EducationEditModel> educationsModel);
    }
}
