namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.CurriculumVitae;
    using System;
    using System.Threading.Tasks;

    public class PersonalDetailsService : DbService, IPersonalDetailsService
    {

        public PersonalDetailsService(JobFinderDbContext dbContext) 
            : base(dbContext)
        {

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

            await this.DbContext.AddAsync(personalInfo);
            await this.DbContext.SaveChangesAsync();

            return personalInfo.Id;
        }

        public async Task<bool> UpdateAsync(int personalDetailsId, string firstName, string middleName, string lastName, DateTime birthdate, Gender gender, string email, string phone, Country country, Country citizenShip, string city)
        {
            var personalDetails = await this.DbContext.FindAsync<PersonalDetails>(personalDetailsId);

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

            await this.DbContext.SaveChangesAsync();

            return true;
        }
    }
}
