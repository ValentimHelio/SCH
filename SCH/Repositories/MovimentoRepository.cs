using Microsoft.EntityFrameworkCore;
using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Repositories
{
    public class MovimentoRepository : Repository<Movimento>, IMovimentoRepository
    {
        private readonly AppDbContext _context;

        public MovimentoRepository(AppDbContext context) : base(context) { _context = context; }

        public async Task<Movimento> GetMovimentoById(int id)
        {
            return await _context.Movimentos.Include(s => s.servico).Include(c => c.cliente).Where(s => s.MovimentoId == id).FirstOrDefaultAsync();
        }

    }
}
