using AutoMapper;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.JobAd;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.JobAds
{
    public class JobAdCreateViewModel : IMapTo<JobAdCreateDTO>,
        IMapTo<JobAdCategoryDTO>,
        IHaveCustomMappings
    {
        [Required]
        [StringLength(90, MinimumLength = 6)]
        public string Position { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public int LocationId { get; set; }

        public int JobCategoryId { get; set; }

        public int JobEngagementId { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

        public int? CurrencyId { get; set; }

        public bool Intership { get; set; }

        public int WorkplaceTypeId { get; set; }

        public IEnumerable<int> SoftSkills { get; set; }

        public IEnumerable<int> ITAreas { get; set; }

        public IEnumerable<int> TechStacks { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdCreateViewModel, SalaryPropertiesDTO>()
                .ForMember(dto => dto.HasCurrencyType, o => o.MapFrom(vm => vm.CurrencyId.HasValue));
        }
    }
}
