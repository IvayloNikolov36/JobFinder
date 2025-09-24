namespace JobFinder.Transfer.DTOs.Company;

public class CompanyListingDTO : BasicDTO
{
    public string Logo { get; set; }

    public int Employees { get; set; }

    public int Ads { get; set; }
}
