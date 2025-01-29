using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly AppDbContext _context;

        public EmpresaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Empresa> GetEmpresas => _context.Empresas;

        public Empresa GetEmpresaById(int id)
        {
            return _context.Empresas.FirstOrDefault(l => l.EmpresaId == id);
        }
    }
}
