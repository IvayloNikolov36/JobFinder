using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.CV
{
    public interface IWorkExperienceInfosService
    {
        Task<UpdateResult> UpdateAsync(string cvId, IEnumerable<WorkExperienceEditModel> workExperienceModels);
    }
}
