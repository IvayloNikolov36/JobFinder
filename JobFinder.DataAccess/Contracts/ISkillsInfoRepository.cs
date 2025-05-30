namespace JobFinder.DataAccess.Contracts;

public interface ISkillsInfoRepository
{
    Task Delete(string cvId);
}
