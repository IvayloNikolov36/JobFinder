using JobFinder.Web.Models.CVModels;

namespace JobFinder.Services.CV;

public interface ISkillsInfosService
{
    Task Update(SkillsEditModel skillsModel);
}
