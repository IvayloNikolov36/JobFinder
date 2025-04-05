namespace JobFinder.Transfer.DTOs
{
    public class JobSubscriptionDTO
    {
        public int Id { get; set; }

        public string RecurringType { get; set; }

        public string JobCategory { get; set; }

        public string JobEngagement { get; set; }

        public string Location { get; set; }

        public bool SpecifiedSalary { get; set; }

        public bool Intership { get; set; }

        public string SearchTerm { get; set; }
    }
}
