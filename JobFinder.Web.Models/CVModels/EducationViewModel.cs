using JobFinder.Data.Models.CV;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;
using JobFinder.Web.Models.Common;
using System;

namespace JobFinder.Web.Models.CVModels;

public class EducationViewModel : IMapFrom<EducationInfoEntity>, IMapFrom<EducationInfoDTO>
{
    public int Id { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string Organization { get; set; }

    public string Location { get; set; }

    public BasicViewModel EducationLevel { get; set; }

    public string Major { get; set; }

    public string MainSubjects { get; set; }
}
