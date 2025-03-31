using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using SCH.Models;
using static SCH.Repositories.Interfaces.IRepository;

namespace SCH.Repositories.Interfaces;

public interface IServicoRepository : IRepository<Servico>
{
    Task<IEnumerable<Servico>> GetAllServico();
    Task<Servico> GetServicoById(int id);
    SelectList SelectListServicos();
    Task<PagingList<Servico>> GetServicoPagindo(string filter, int pageindex, string sort);
}
