using JobFinder.Common.Exceptions;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using System.Linq;

namespace JobFinder.Business.AnonymousProfile;

public class AnonymousProfileRules : IAnonymousProfileRules
{
    private const int ItJobCategoryId = 3;

    public void ValidateAnonymousProfileAppearanceData(AnonymousProfileAppearanceDTO profileAppearance)
    {
        if (profileAppearance.JobCategoryId == ItJobCategoryId)
        {
            if (!profileAppearance.ITAreas.Any())
            {
                throw new ActionableException("You have to specify at least one IT area!");
            }

            if (!profileAppearance.TechStacks.Any())
            {
                throw new ActionableException("You have to specify at least one Tech Stack!");
            }
        }
        else
        {
            if (profileAppearance.ITAreas.Any() || profileAppearance.TechStacks.Any())
            {
                throw new ActionableException("You have to specify Job Category 'IT development' to be allowed to specify IT areas and Tech Stack!");
            }
        }
    }
}
