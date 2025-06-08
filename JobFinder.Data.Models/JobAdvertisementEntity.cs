using JobFinder.Data.Models.Common;
using JobFinder.Data.Models.Nomenclature;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Data.Models
{
    public partial class JobAdvertisementEntity : BaseEntity<int>
    {
        public JobAdvertisementEntity()
        {
            this.JobAdApplications = new List<JobAdApplicationEntity>();
        }

        [Required]
        [StringLength(90, MinimumLength = 6)]
        public string Position { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public int LocationId { get; set; }
        public CityEntity Location { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public int? CurrencyId { get; set; }
        public CurrencyEntity Currency { get; set; }

        public int JobCategoryId { get; set; }
        public JobCategoryEntity JobCategory { get; set; }

        public int JobEngagementId { get; set; }
        public JobEngagementEntity JobEngagement { get; set; }

        public int WorkplaceTypeId { get; set; }
        public WorkplaceTypeEntity WorkplaceType { get; set; }

        public bool Intership { get; set; }

        public int PublisherId { get; set; }
        public CompanyEntity Publisher { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsActive { get; set; }

        public ICollection<JobAdApplicationEntity> JobAdApplications { get; set; }
    }
}
