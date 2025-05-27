namespace JobFinder.DataAccess.Contracts;

public interface IWorkExperienceRepository
{
    Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> workExperienceId);

    Task DisassociateFromAnonymousProfile(string cvId);
}
