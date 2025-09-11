using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;
using JobFinder.Web.Models.CvModels;
using System;

namespace JobFinder.Web.Models.AdApplication;

public class JobApplicationInfoViewModel : IMapFrom<JobAdApplicationInfoDTO>
{
    public int Id { get; set; }

    public int JobAdId { get; set; }

    public string Applicant { get; set; }

    public CvBasicDetailsViewModel Cv { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public DateTime AppliedOn { get; set; }

    public bool IsPreviewed { get; set; }

    public DateTime? PreviewDate { get; set; }
}
