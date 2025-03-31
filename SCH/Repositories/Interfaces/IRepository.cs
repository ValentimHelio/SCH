namespace SCH.Repositories.Interfaces;

public interface IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        T Add(T entity);
        T Update(T entity);
        T Delete(int id);
    }
}
