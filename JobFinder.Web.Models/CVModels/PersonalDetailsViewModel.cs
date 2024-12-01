namespace JobFinder.Web.Models.CVModels
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using System;

    public class PersonalDetailsViewModel : IHaveCustomMappings
    {
        public string CurriculumVitaeId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public BasicValueViewModel Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public BasicValueViewModel CitizenShip { get; set; }

        public BasicValueViewModel Country { get; set; }

        public string City { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PersonalDetails, PersonalDetailsViewModel>()
               .ForMember(x => x.Gender, m => m.MapFrom(m =>
                    new BasicValueViewModel((int)m.Gender, m.Gender.ToString())))
               .ForMember(x => x.CitizenShip, m => m.MapFrom(m =>
                    new BasicValueViewModel((int)m.CitizenShip, m.CitizenShip.ToString())))
               .ForMember(x => x.Country, m => m.MapFrom(m => new BasicValueViewModel((int)m.Country, m.Country.ToString())));
        }
    }
}
