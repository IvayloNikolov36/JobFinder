using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.CV;

public interface IPersonalInfosService
{
    Task Update(string cvId, PersonalInfoEditModel personalInfo);
}
