namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PersonalDetailsInputModel : IMapTo<PersonalInfo>
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

        public GenderEnum Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public BasicViewModel CitizenShip { get; set; }

        public BasicViewModel Country { get; set; }

        public string City { get; set; }
    }
}
