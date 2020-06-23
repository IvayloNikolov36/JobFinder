namespace JobFinder.Services.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using System;
    using System.Threading.Tasks;

    public interface IPersonalDetailsService
    {
        Task<T> GetAsync<T>(string cvId);

        Task<int> CreateAsync(
            string cvId,
            string firstName,
            string middleName,
            string lastName,
            DateTime birthdate,
            Gender gender,
            string email,
            string phone,
            Country country,
            Country citizenShip,
            string city);

        Task<bool> UpdateAsync(
            int personalDetailsId,
            string firstName,
            string middleName,
            string lastName,
            DateTime birthdate,
            Gender gender,
            string email,
            string phone,
            Country country,
            Country citizenShip,
            string city);
    }
}
