namespace JobFinder.Web.Models.Account
{
    using AutoMapper;
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;
    using System.ComponentModel.DataAnnotations;
    using static JobFinder.Common.Constants;
    using static JobFinder.Common.MessageConstants;

    public class RegisterModel : IHaveCustomMappings
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, ErrorMessage = ErrorModelPropertyMessage, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(this.Password), ErrorMessage = ErrorModelPropertyMessage)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(MaxNameLength, ErrorMessage = ErrorModelPropertyMessage, MinimumLength = MinNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(MaxNameLength, ErrorMessage = ErrorModelPropertyMessage, MinimumLength = MinNameLength)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(MaxNameLength, ErrorMessage = ErrorModelPropertyMessage, MinimumLength = MinNameLength)]
        public string LastName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<RegisterModel, UserEntity>()
                .ForMember(e => e.UserName, o => o.MapFrom(vm => vm.Email));
        }
    }
}
