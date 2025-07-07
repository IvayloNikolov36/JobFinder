using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services.CV;

public interface ISkillsInfosService
{
    Task Update(string cvId, SkillsEditModel skillsModel);
}
