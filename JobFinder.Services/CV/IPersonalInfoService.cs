using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.CV;

public interface IPersonalInfoService
{
    Task Update(string cvId, PersonalInfoEditModel personalInfo);
}
