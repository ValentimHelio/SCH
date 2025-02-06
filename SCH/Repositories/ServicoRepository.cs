using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Repositories
{
    public class ServicoRepository : Repository<Servico>, IServicoRepository
    {
        private readonly AppDbContext _context;

        public ServicoRepository(AppDbContext context) : base(context) { }
    }
}
