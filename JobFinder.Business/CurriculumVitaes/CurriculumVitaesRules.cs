using JobFinder.Common.Exceptions;

namespace JobFinder.Business.CurriculumVitaes;

public class CurriculumVitaesRules : ICurriculumVitaesRules
{
    public void ValidateAnonymousProfileCreation(bool hasActivatedAnonymousProfile)
    {
        if (hasActivatedAnonymousProfile)
        {
            throw new ActionableException("You can have only one activated anonymous profile!");
        }
    }
}
