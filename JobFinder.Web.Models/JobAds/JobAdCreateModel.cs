namespace JobFinder.Web.Models.JobAds
{
    using AutoMapper;
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;
    using JobFinder.Transfer.DTOs;
    using System.ComponentModel.DataAnnotations;

    public class JobAdCreateModel : IMapTo<JobAdvertisementEntity>, IHaveCustomMappings
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

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<JobAdCreateModel, SalaryPropertiesDTO>()
                .ForMember(dto => dto.HasCurrencyType, o => o.MapFrom(vm => vm.CurrencyId.HasValue));
        }
    }
}
