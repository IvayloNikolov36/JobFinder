﻿namespace JobFinder.Data.Models.CV
{
    using JobFinder.Data.Models.Common;
    using JobFinder.Data.Models.Nomenclature;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PersonalInfoEntity : BaseEntity<int>
    {
        [Required]
        public string CurriculumVitaeId { get; set; }

        public CurriculumVitaeEntity CurriculumVitae { get; set; }

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

        public GenderEntity Gender { get; set; }

        public DateTime Birthdate { get; set; }

        [Required]
        public int CitizenshipId { get; set; }

        public CitizenshipEntity Citizenship { get; set; }

        [Required]
        public int CountryId { get; set; }

        public CountryEntity Country { get; set; }

        public string City { get; set; }
    }
}