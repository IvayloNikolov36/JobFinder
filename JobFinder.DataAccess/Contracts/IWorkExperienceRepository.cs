namespace JobFinder.DataAccess.Contracts;

public interface IWorkExperienceRepository
{
    Task SetIncludeInAnonymousProfile(string cvId, int workExperienceId);
}
