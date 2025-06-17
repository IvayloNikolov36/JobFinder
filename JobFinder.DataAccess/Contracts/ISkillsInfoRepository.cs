using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.DataAccess.Contracts;

public interface ISkillsInfoRepository
{
    Task Update(SkillsInfoEditDTO skillsInfoDto);

    Task Delete(string cvId);
}
