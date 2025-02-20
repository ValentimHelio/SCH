using SCH.Models;
using static SCH.Repositories.Interfaces.IRepository;

namespace SCH.Repositories.Interfaces
{
    public interface IMovimentoRepository : IRepository<Movimento>
    {
        Task<Movimento> GetMovimentoById(int id);
    }
}
