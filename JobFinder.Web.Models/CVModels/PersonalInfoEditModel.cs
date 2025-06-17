using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.CvModels
{
    public class PersonalInfoEditModel : IMapTo<PersonalInfoEditDTO>
    {
        public int Id { get; set; }

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

        public int GenderId { get; set; }

        public DateTime Birthdate { get; set; }

        public int CitizenShipId { get; set; }

        public int CountryId { get; set; }

        public string City { get; set; }
    }
}
