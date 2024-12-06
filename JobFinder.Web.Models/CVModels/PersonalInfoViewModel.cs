namespace JobFinder.Web.Models.CVModels
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using System;

    public class PersonalInfoViewModel : IMapFrom<PersonalInfo>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public BasicViewModel Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public BasicViewModel Citizenship { get; set; }

        public BasicViewModel Country { get; set; }

        public string City { get; set; }
    }
}
