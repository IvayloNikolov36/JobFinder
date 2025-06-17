using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.CV;

public interface IPersonalInfosService
{
    Task Update(PersonalInfoEditModel personalInfo);
}
