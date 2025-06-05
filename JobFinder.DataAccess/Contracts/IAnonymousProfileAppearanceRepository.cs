using JobFinder.Transfer.DTOs.AnonymousProfile;

namespace JobFinder.DataAccess.Contracts;

public interface IAnonymousProfileAppearanceRepository
{
    Task Create(string cvId, AnonymousProfileAppearanceCreateDTO profileAppearance);
}
