namespace JobFinder.Transfer.DTOs.CV;

public class PersonalInfoDTO
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public BasicDTO Gender { get; set; }

    public DateTime Birthdate { get; set; }

    public BasicDTO Citizenship { get; set; }

    public BasicDTO Country { get; set; }

    public string City { get; set; }
}
