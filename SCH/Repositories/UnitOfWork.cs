using SCH.Context;
using SCH.Repositories.Interfaces;

namespace SCH.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IServicoRepository _servicoRepo;
    private IMovimentoRepository _movimentoRepo;
    private IEmpresaRepository _empresaRepo;
    private IClienteRepository _clienteRepo;
    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IServicoRepository servicoRepository
    {
        get
        {
            return _servicoRepo = _servicoRepo ?? new ServicoRepository(_context);
        }
    }

    public IMovimentoRepository movimentoRepository
    {
        get
        {
            return _movimentoRepo = _movimentoRepo ?? new MovimentoRepository(_context);
        }
    }

    public IEmpresaRepository empresaRepository
    {
        get
        {
            return _empresaRepo = _empresaRepo ?? new EmpresaRepository(_context);
        }
    }

    public IClienteRepository clienteRepository
    {
        get
        {
            return _clienteRepo = _clienteRepo ?? new ClienteRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
