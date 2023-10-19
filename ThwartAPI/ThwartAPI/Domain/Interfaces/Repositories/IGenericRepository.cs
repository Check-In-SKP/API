namespace ThwartAPI.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task<T> GetByIdAsync(TKey id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(TKey id);
    }
}
