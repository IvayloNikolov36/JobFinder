using JobFinder.Transfer.DTOs.AnonymousProfile;

namespace JobFinder.Business.AnonymousProfile;

public interface IAnonymousProfileRules
{
    void ValidateAnonymousProfileAppearanceData(AnonymousProfileAppearanceDTO profileAppearance);
}
