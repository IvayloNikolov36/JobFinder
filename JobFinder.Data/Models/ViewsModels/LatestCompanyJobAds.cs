namespace JobFinder.Data.Models.ViewsModels
{
    public class LatestCompanyJobAds
    {
        public int Id { get; set; } //Company id

        public string Name { get; set; } //Company name

        public string Logo { get; set; } //Company logo

        public string JobAdsIds { get; set; }

        public string Positions { get; set; }
    }
}
