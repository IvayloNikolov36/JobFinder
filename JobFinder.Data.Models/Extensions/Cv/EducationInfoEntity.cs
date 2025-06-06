﻿using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.Data.Models.CV;

public partial class EducationInfoEntity : IMapFrom<EducationInfoEditDTO>,
    IMapTo<EducationInfoDTO>,
    IMapTo<EducationInfoEditDTO>,
    IMapFrom<EducationInfoInputDTO>
{

}
