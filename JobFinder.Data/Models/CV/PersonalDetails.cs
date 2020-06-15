namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PersonalDetails : BaseModel<int>
    {
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

        public Gender Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public Country CitizenShip { get; set; }

        public Country Country { get; set; }

        public string City { get; set; }
    }
}
