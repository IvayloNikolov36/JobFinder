namespace JobFinder.Web.Models.Account
{
    using AutoMapper;
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;
    using System.ComponentModel.DataAnnotations;

    public class RegisterCompanyModel : RegisterModel, IMapTo<UserEntity>, IHaveCustomMappings
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 9)]
        public string Bulstat { get; set; }

        [Required]
        [Url]
        public string Logo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<RegisterCompanyModel, CompanyEntity>()
                .ForMember(e => e.Name, o => o.MapFrom(m => m.CompanyName));
        }
    }
}
