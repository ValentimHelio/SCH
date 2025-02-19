using SCH.Models;
using System.Threading.Tasks;
using static SCH.Repositories.Interfaces.IRepository;

namespace SCH.Repositories.Interfaces
{
    public interface IServicoRepository : IRepository<Servico>
    {
        Task<IEnumerable<Servico>> GetAllServico();
        Task<Servico> GetServicoById(int id);
    }
}
