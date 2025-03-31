using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Repositories;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    private readonly AppDbContext _context;
    public ClienteRepository(AppDbContext context) : base(context) { _context = context; }

    public async Task<PagingList<Cliente>> GetClientePagindo(string filter, int pageindex, string sort)
    {
        var resultado = _context.Clientes.AsQueryable();
        if (!string.IsNullOrWhiteSpace(filter))
        {
            resultado = resultado.Where(p => p.NomeCliente.Contains(filter));
        }

        var model = await PagingList.CreateAsync(resultado, 10, pageindex, sort, "NomeCliente");
        model.RouteValue = new RouteValueDictionary { { "filter", filter } };
        return model;
    }

    public SelectList SelectListClientes()
    {
        return new SelectList(_context.Clientes, "ClienteId", "NomeCliente");
    }

}
