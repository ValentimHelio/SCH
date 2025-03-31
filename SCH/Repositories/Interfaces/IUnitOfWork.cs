namespace SCH.Repositories.Interfaces;

public interface IUnitOfWork
{
    IServicoRepository servicoRepository { get; }
    IMovimentoRepository movimentoRepository { get; }
    IEmpresaRepository empresaRepository { get; }
    IClienteRepository clienteRepository { get; }
    void Commit();
    Task CommitAsync();
}
