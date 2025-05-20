namespace JobFinder.DataAccess.Contracts;

public interface ILanguageInfoRepository
{
    Task SetIncludeInAnonymousProfile(string cvId, int languageInfoId);
}
