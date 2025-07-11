﻿namespace JobFinder.Transfer.DTOs.Cv;

public class SkillsInfoEditDTO
{
    public int Id { get; set; }

    public string ComputerSkills { get; set; }

    public string OtherSkills { get; set; }

    public bool HasManagedPeople { get; set; }

    public IEnumerable<int> DrivingLicenseCategoryIds { get; set; }
}
