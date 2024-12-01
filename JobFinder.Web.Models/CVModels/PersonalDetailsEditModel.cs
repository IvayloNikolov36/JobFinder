namespace JobFinder.Web.Models.CVModels
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.Mappings;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PersonalDetailsEditModel : IHaveCustomMappings
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public int Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public int CitizenShip { get; set; }

        public int Country { get; set; }

        public string City { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PersonalDetailsEditModel, PersonalDetails>()
                .ForMember(e => e.Country, o => o.MapFrom(vm => (CountryEnum)vm.Country))
                .ForMember(e => e.CitizenShip, o => o.MapFrom(vm => (CountryEnum)vm.CitizenShip))
                .ForMember(e => e.Gender, o => o.MapFrom(vm => (GenderEnum)vm.Gender));
        }
    }
}
