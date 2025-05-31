namespace JobFinder.Transfer.DTOs.CV;

public class PersonalInfoEditDTO
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public int GenderId { get; set; }

    public DateTime Birthdate { get; set; }

    public int CitizenShipId { get; set; }

    public int CountryId { get; set; }

    public string City { get; set; }
}
