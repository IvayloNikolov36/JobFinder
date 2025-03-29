namespace JobFinder.Web.Models.Account
{
    using AutoMapper;
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;
    using System.ComponentModel.DataAnnotations;

    public class RegisterModel : IHaveCustomMappings
    {
        private const int PasswordMinLength = 6;
        private const int PasswordMaxLength = 35;
        private const int MinNameLength = 2;
        private const int MaxNameLength = 25;
        private const string ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, ErrorMessage = ErrorMessage, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(this.Password), ErrorMessage = ErrorMessage)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(MaxNameLength, ErrorMessage = ErrorMessage, MinimumLength = MinNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(MaxNameLength, ErrorMessage = ErrorMessage, MinimumLength = MinNameLength)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(MaxNameLength, ErrorMessage = ErrorMessage, MinimumLength = MinNameLength)]
        public string LastName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<RegisterModel, UserEntity>()
                .ForMember(e => e.UserName, o => o.MapFrom(vm => vm.Email));
        }
    }
}
