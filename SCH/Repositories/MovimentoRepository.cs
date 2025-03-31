using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Repositories;

public class MovimentoRepository : Repository<Movimento>, IMovimentoRepository
{
    private readonly AppDbContext _context;

    public MovimentoRepository(AppDbContext context) : base(context) { _context = context; }

    public async Task<Movimento> GetMovimentoById(int id)
    {
        return await _context.Movimentos.Include(s => s.servico).Include(c => c.cliente).Where(s => s.MovimentoId == id).FirstOrDefaultAsync();
    }

    public async Task<PagingList<Movimento>> GetMovimentoPagindo(string filter, int pageindex, string sort)
    {
        var resultado = _context.Movimentos.Include(s => s.servico).Include(c => c.cliente).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            resultado = resultado.Where(p => p.cliente.NomeCliente.Contains(filter));
        }

        var model = await PagingList.CreateAsync(resultado, 10, pageindex, sort, "Valor_Hora");
        model.RouteValue = new RouteValueDictionary { { "filter", filter } };
        return model;
    }
}
