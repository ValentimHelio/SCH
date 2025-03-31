using ReflectionIT.Mvc.Paging;
using SCH.Models;
using static SCH.Repositories.Interfaces.IRepository;

namespace SCH.Repositories.Interfaces;

public interface IMovimentoRepository : IRepository<Movimento>
{
    Task<Movimento> GetMovimentoById(int id);
    Task<PagingList<Movimento>> GetMovimentoPagindo(string filter, int pageindex, string sort);
}
