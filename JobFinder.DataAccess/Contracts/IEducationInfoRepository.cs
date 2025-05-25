namespace JobFinder.DataAccess.Contracts;

public interface IEducationInfoRepository
{
    Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> educationInfoIds);
}
