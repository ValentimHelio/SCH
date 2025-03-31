using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Repositories;

public class ServicoRepository : Repository<Servico>, IServicoRepository
{
    private readonly AppDbContext _context;

    public ServicoRepository(AppDbContext context) : base(context) { _context = context; }

    public async Task<IEnumerable<Servico>> GetAllServico()
    {
        return await _context.Servicos.Include(e => e.empresa).ToListAsync();
    }

    public async Task<Servico> GetServicoById(int id)
    {
        return await _context.Servicos.Include(e => e.empresa).Where(s => s.ServicoId == id).FirstOrDefaultAsync();
    }

    public SelectList SelectListServicos()
    {
        return new SelectList(_context.Servicos, "ServicoId", "Descricao");
    }

    public async Task<PagingList<Servico>> GetServicoPagindo(string filter, int pageindex, string sort)
    {
        var resultado = _context.Servicos.Include(e => e.empresa).AsQueryable();
        if (!string.IsNullOrWhiteSpace(filter))
        {
            resultado = resultado.Where(p => p.Descricao.Contains(filter));
        }

        var model = await PagingList.CreateAsync(resultado, 10, pageindex, sort, "Descricao");
        model.RouteValue = new RouteValueDictionary { { "filter", filter } };
        return model;
    }
}
