namespace CheckInMonitorAPI.Services.Interfaces
{
    public interface IGenericService<T, TKey> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TKey id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TKey id);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        bool EntityExist(TKey id);
        IQueryable<T> Query();
    }
}
