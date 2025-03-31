using Microsoft.EntityFrameworkCore;
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
}
