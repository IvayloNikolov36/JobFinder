﻿using JobFinder.Data.Models.Cv;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Web.Models.Common;
using System;

namespace JobFinder.Web.Models.CvModels;

public class EducationViewModel : IMapFrom<EducationInfoEntity>, IMapFrom<EducationInfoDTO>
{
    public int Id { get; set; }

    public bool? IncludeInAnonymousProfile { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string Organization { get; set; }

    public string Location { get; set; }

    public BasicViewModel EducationLevel { get; set; }

    public string Major { get; set; }

    public string MainSubjects { get; set; }
}
