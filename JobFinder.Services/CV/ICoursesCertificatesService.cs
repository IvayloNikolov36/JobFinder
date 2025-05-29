using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CVModels;

namespace JobFinder.Services.CV;

public interface ICoursesCertificatesService
{
    Task<IEnumerable<CourseInfoViewModel>> All(string cvId);

    Task<UpdateResult> Update(string cvId, IEnumerable<CourseSertificateEditModel> coursesInfo);

    Task Delete(int id);
}
