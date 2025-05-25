namespace JobFinder.DataAccess.Contracts;

public interface ILanguageInfoRepository
{
    Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> languageInfoIds);
}
