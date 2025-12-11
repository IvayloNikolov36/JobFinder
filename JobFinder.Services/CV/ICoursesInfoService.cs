using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.Cv;

public interface ICoursesInfoService
{
    Task<IEnumerable<CourseInfoViewModel>> All(string cvId);

    Task<UpdateResult<int>> Update(string cvId, IEnumerable<CourseSertificateEditModel> coursesInfo);

    Task Delete(int id);
}
