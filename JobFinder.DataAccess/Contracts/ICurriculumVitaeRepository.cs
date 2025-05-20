namespace JobFinder.DataAccess.Contracts;

public interface ICurriculumVitaeRepository
{
    Task<string> GetUserId(string curriculumVitaeId);
}
