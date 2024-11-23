namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PersonalDetailsEditModel
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

        public Gender Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public Country CitizenShip { get; set; }

        public Country Country { get; set; }

        public string City { get; set; }
    }
}
