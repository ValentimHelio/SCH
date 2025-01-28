using SCH.Models;

namespace SCH.Repositories.Interfaces
{
    public interface IEmpresaRepository
    {
        IEnumerable<Empresa> GetEmpresas { get; }
    }
}
