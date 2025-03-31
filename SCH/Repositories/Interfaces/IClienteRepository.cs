using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using SCH.Models;
using static SCH.Repositories.Interfaces.IRepository;

namespace SCH.Repositories.Interfaces;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<PagingList<Cliente>> GetClientePagindo(string filter, int pageindex, string sort);
    SelectList SelectListClientes();
}
