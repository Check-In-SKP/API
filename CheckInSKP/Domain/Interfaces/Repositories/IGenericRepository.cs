﻿namespace CheckInSKP.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(TKey id);
        Task UpdateAsync(T entity);
        Task RemoveAsync(TKey id);
        Task<bool> ExistsAsync(TKey id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithPaginationAsync(int page, int pageSize);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveRangeAsync(IEnumerable<TKey> entities);
        IQueryable<T> Query();
    }
}
