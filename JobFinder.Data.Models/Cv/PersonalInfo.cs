namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PersonalInfo : BaseModel<int>
    {
        [Required]
        public string CurriculumVitaeId { get; set; }

        public CurriculumVitae CurriculumVitae { get; set; }

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

        [Required]
        public int GenderId { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthdate { get; set; }

        [Required]
        public int CitizenshipId { get; set; }

        public Citizenship Citizenship { get; set; }

        [Required]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        public string City { get; set; }
    }
}
