using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using System;

namespace JobFinder.Web.Models.JobAds
{
    public class CompanyJobAdViewModel : IMapFrom<CompanyJobAdDTO>
    {
        public int Id { get; set; }

        public string Position { get; set; }

        public string JobCategory { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public string Currency { get; set; }

        public string Location { get; set; }

        public int ApplicationsSent { get; set; }

        public int NotPreviewedApplications { get; set; }

        public int LifecycleStatusId { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
