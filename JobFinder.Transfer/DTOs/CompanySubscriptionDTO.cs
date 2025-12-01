namespace JobFinder.Transfer.DTOs
{
    public class CompanySubscriptionDTO
    {
        public int CompanyId { get; set; }

        public int? CompanyLogoImageId { get; set; }

        public string CompanyLogo { get; set; }

        public string CompanyName { get; set; }
    }
}
