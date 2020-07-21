namespace JobFinder.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IJobEngagementsService
    {
        Task<IEnumerable<T>> AllAsync<T>();
    }
}
