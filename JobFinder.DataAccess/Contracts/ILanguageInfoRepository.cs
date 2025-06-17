using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.DataAccess.Contracts;

public interface ILanguageInfoRepository
{
    Task<IEnumerable<LanguageInfoEditDTO>> Update(string cvId, IEnumerable<LanguageInfoEditDTO> languageInfoDtos);

    Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> languageInfoIds);

    Task DisassociateFromAnonymousProfile(string cvId);

    void Delete(string cvId);
}
