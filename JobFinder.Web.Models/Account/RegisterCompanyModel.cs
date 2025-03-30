using AutoMapper;
using JobFinder.Data.Models;
using JobFinder.Services.Mappings;
using System.ComponentModel.DataAnnotations;
using static JobFinder.Common.Constants;
using static JobFinder.Common.MessageConstants;

namespace JobFinder.Web.Models.Account
{
    public class RegisterCompanyModel : RegisterModel, IMapTo<UserEntity>, IHaveCustomMappings
    {
        [Required]
        [StringLength(CompanyNameMaxLength, ErrorMessage = ErrorModelPropertyMessage, MinimumLength = CompanyNameMinLegth)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(BulstatMaxLength, ErrorMessage = ErrorModelPropertyMessage, MinimumLength = BulstatMinLength)]
        public string Bulstat { get; set; }

        [Required]
        [Url]
        public string Logo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<RegisterCompanyModel, CompanyEntity>()
                .ForMember(e => e.Name, o => o.MapFrom(m => m.CompanyName));

            configuration.CreateMap<RegisterCompanyModel, UserEntity>()
                .ForMember(e => e.UserName, o => o.MapFrom(vm => vm.Email));
        }
    }
}
