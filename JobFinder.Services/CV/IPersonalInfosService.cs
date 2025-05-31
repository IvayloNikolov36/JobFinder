using JobFinder.Web.Models.CVModels;

namespace JobFinder.Services.CV;

public interface IPersonalInfosService
{
    Task Update(PersonalInfoEditModel personalInfo);
}
