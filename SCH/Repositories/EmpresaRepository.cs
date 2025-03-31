using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Repositories;

public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
{
    private readonly AppDbContext _context;
    public EmpresaRepository(AppDbContext context) : base(context) { _context = context; }

    public async Task<PagingList<Empresa>> GetEmpresaPagindo(string filter, int pageindex, string sort)
    {
        var resultado = _context.Empresas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            resultado = resultado.Where(p => p.NomeEmpresa.Contains(filter));
        }

        var model = await PagingList.CreateAsync(resultado, 10, pageindex, sort, "NomeEmpresa");
        model.RouteValue = new RouteValueDictionary { { "filter", filter } };
        return model;
    }

    public SelectList SelectListEmpresas()
    {
        return new SelectList(_context.Empresas, "EmpresaId", "NomeEmpresa");
    }
}
