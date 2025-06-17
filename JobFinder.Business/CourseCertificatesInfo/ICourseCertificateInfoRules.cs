using JobFinder.Transfer.DTOs.Cv;
using System.Collections.Generic;

namespace JobFinder.Business.CourseCertificatesInfo;

public interface ICourseCertificateInfoRules
{
    void ValidateData(IEnumerable<CourseCertificateInputDTO> courseCertificates);
}
