namespace JobFinder.Transfer.DTOs.Company;

public class CompanyListingDTO : BasicDTO
{
    public int? LogoId { get; set; }

    public int Employees { get; set; }

    public int Ads { get; set; }

    public bool Subscription { get; set; }
}
