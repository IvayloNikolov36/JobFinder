using JobFinder.Common.Exceptions;
using JobFinder.Transfer.DTOs.Cv;
using System.Collections.Generic;

namespace JobFinder.Business.CourseCertificatesInfo
{
    public class CourseCertificateInfoRules : ICourseCertificateInfoRules
    {
        public void ValidateData(IEnumerable<CourseCertificateInputDTO> courseCertificates)
        {
            if (courseCertificates == null)
            {
                throw new ActionableException("CourseCertificats cannot be null!");
            }

            foreach (CourseCertificateInputDTO courseInfo in courseCertificates)
            {
                if (string.IsNullOrWhiteSpace(courseInfo?.CourseName))
                {
                    throw new ActionableException("Course name cannot be null, empty string or whitespace!");
                }

                if (courseInfo?.CertificateUrl == null)
                {
                    throw new ActionableException("Course certificate url cannot be null!");
                }
            }
        }
    }
}
