namespace JobFinder.DataAccess.Contracts;

public interface IEducationInfoRepository
{
    Task SetIncludeInAnonymousProfile(string cvId, int educationInfoId);
}
