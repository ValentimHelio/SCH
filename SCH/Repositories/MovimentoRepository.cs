using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Repositories
{
    public class MovimentoRepository : Repository<Movimento>, IMovimentoRepository
    {
        private readonly AppDbContext _context;

        public MovimentoRepository(AppDbContext context) : base(context) { }
    }
}
