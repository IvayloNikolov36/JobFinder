namespace JobFinder.Transfer.DTOs.Company;

public class CompanyDetailsUserDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Logo { get; set; }

    public int ActiveAdsCount { get; set; }

    public bool HasSubscription { get; set; }
}
