using JobFinder.Data.Models.Cv;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;
using System;

namespace JobFinder.Web.Models.CvModels;

public class PersonalInfoViewModel :
    IMapFrom<PersonalInfoDTO>,
    IMapFrom<PersonalInfoEntity>
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
