namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PersonalDetailsService : IPersonalDetailsService
    {
        private readonly IRepository<PersonalInfo> repository;
        private readonly IMapper mapper;

        public PersonalDetailsService(
            IRepository<PersonalInfo> personalDetailsRepository,
            IMapper mapper) 
        {
            this.repository = personalDetailsRepository;
            this.mapper = mapper;
        }

        public async Task<T> GetAsync<T>(string cvId)
        {
            T personalDetails = await this.repository.AllAsNoTracking()
                .Where(pd => pd.CurriculumVitaeId == cvId)
                .To<T>()
                .FirstOrDefaultAsync();

            return personalDetails;
        }

        // TODO: refactor or delete if it is not used
        //public async Task<int> CreateAsync(string cvId, string firstName, string middleName, string lastName, DateTime birthdate, GenderEnum gender, string email, string phone, CountryEnum country, CountryEnum citizenShip, string city)
        //{
        //    PersonalDetails personalInfo = new()
        //    {
        //        CurriculumVitaeId = cvId,
        //        FirstName = firstName,
        //        MiddleName = middleName,
        //        LastName = lastName,
        //        Birthdate = birthdate,
        //        Gender = gender,
        //        Email = email,
        //        Phone = phone,
        //        Country = country,
        //        CitizenShip = citizenShip,
        //        City = city
        //    };

        //    await this.repository.AddAsync(personalInfo);
        //    await this.repository.SaveChangesAsync();

        //    return personalInfo.Id;
        //}

        public async Task<bool> UpdateAsync(string cvId, PersonalDetailsEditModel personalDetails)
        {
            PersonalInfo personalDetailsFromDb = await this.repository
                .FirstOrDefaultAsync(pd => pd.CurriculumVitaeId == cvId);

            if (personalDetailsFromDb == null)
            {
                return false;
            }

            this.mapper.Map(personalDetails, personalDetailsFromDb);

            this.repository.Update(personalDetailsFromDb);
            await this.repository.SaveChangesAsync();

            return true;
        }
    }
}
