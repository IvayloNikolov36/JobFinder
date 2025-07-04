﻿namespace JobFinder.Transfer.DTOs.Cv;

public class EducationInfoInputDTO
{
    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string Organization { get; set; }

    public string Location { get; set; }

    public int EducationLevelId { get; set; }

    public string Major { get; set; }

    public string MainSubjects { get; set; }
}
