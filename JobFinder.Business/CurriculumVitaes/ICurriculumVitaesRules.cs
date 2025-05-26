namespace JobFinder.Business.CurriculumVitaes;

public interface ICurriculumVitaesRules
{
    void ValidateAnonymousProfileCreation(bool hasActivatedAnonymousProfile);
}
