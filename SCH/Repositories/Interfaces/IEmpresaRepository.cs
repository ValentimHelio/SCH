using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using SCH.Models;
using static SCH.Repositories.Interfaces.IRepository;

namespace SCH.Repositories.Interfaces;

public interface IEmpresaRepository : IRepository<Empresa>
{
    Task<PagingList<Empresa>> GetEmpresaPagindo(string filter, int pageindex, string sort);
    public SelectList SelectListEmpresas();
}