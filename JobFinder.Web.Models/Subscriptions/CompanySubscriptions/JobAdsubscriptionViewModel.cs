namespace JobFinder.Web.Models.Subscriptions.CompanySubscriptions
{
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;
    using System;

    public class JobAdsubscriptionViewModel : IMapFrom<JobAdEntity>
    {
        public string Position { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
