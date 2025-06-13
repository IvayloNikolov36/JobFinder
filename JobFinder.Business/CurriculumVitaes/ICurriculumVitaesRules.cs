namespace JobFinder.Business.CurriculumVitaes;

public interface ICurriculumVitaesRules
{
    void ValidateAnonymousProfileCanBeCreated(bool hasActivatedAnonymousProfile);
}
