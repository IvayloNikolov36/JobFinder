using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.CV;

namespace JobFinder.Data.Models.CV;

public partial class CourseCertificateEntity : IMapFrom<CourseCertificateSimpleDTO>,
    IMapFrom<CourseCertificateInputDTO>,
    IMapTo<CourseCertificateDTO>,
    IMapTo<CourseCertificateSimpleDTO>
{

}
