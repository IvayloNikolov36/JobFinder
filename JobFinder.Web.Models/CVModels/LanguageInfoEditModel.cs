﻿using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.Web.Models.CVModels;

public class LanguageInfoEditModel : IMapTo<LanguageInfoEditDTO>
{
    public int Id { get; set; }

    public int LanguageTypeId { get; set; }

    public int ComprehensionLevelId { get; set; }

    public int SpeakingLevelId { get; set; }

    public int WritingLevelId { get; set; }
}
