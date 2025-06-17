namespace JobFinder.Business.Cv;

public interface ICvRules
{
    void ValidateAnonymousProfileCanBeCreated(bool hasActivatedAnonymousProfile);
}
