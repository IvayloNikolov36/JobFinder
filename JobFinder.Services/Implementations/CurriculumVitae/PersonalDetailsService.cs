namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Data.Repositories;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PersonalDetailsService : IPersonalDetailsService
    {
        private readonly IRepository<PersonalDetails> repository;

        public PersonalDetailsService(IRepository<PersonalDetails> personalDetailsRepository) 
        {
            this.repository = personalDetailsRepository;
        }

        public async Task<T> GetAsync<T>(string cvId)
        {
            T personalDetails = await this.repository.AllAsNoTracking()
                .Where(pd => pd.CurriculumVitaeId == cvId)
                .To<T>()
                .FirstOrDefaultAsync();

            return personalDetails;
        }

        public async Task<int> CreateAsync(string cvId, string firstName, string middleName, string lastName, DateTime birthdate, Gender gender, string email, string phone, Country country, Country citizenShip, string city)
        {
            var personalInfo = new PersonalDetails
            {
                CurriculumVitaeId = cvId,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Birthdate = birthdate,
                Gender = gender,
                Email = email,
                Phone = phone,
                Country = country,
                CitizenShip = citizenShip,
                City = city
            };

            await this.repository.AddAsync(personalInfo);
            await this.repository.SaveChangesAsync();

            return personalInfo.Id;
        }

        public async Task<bool> UpdateAsync(int personalDetailsId, string firstName, string middleName, string lastName, DateTime birthdate, Gender gender, string email, string phone, Country country, Country citizenShip, string city)
        {
            var personalDetails = await this.repository.FindAsync(personalDetailsId);

            if (personalDetails == null)
            {
                return false;
            }

            personalDetails.FirstName = firstName;
            personalDetails.MiddleName = middleName;
            personalDetails.LastName = lastName;
            personalDetails.Birthdate = birthdate;
            personalDetails.Gender = gender;
            personalDetails.Email = email;
            personalDetails.Phone = phone;
            personalDetails.Country = country;
            personalDetails.CitizenShip = citizenShip;
            personalDetails.City = city;

            await this.repository.SaveChangesAsync();

            return true;
        }
    }
}
