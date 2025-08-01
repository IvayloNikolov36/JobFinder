﻿namespace JobFinder.Transfer.DTOs.Cv;

public class PersonalInfoInputDTO
{
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public int GenderId { get; set; }

    public DateTime Birthdate { get; set; }

    public int CitizenshipId { get; set; }

    public int CountryId { get; set; }

    public string City { get; set; }
}
