using JobFinder.Common.Exceptions;

namespace JobFinder.Business.Cv;

public class CvRules : ICvRules
{
    public void ValidateAnonymousProfileCanBeCreated(bool hasActivatedAnonymousProfile)
    {
        if (hasActivatedAnonymousProfile)
        {
            throw new ActionableException("You can have only one activated anonymous profile!");
        }
    }
}
