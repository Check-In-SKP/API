using CheckInMonitorAPI.Data.Repositories.Interfaces;

namespace CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T, TKey> GetRepository<T, TKey>() where T : class;
        Task CompleteAsync();
    }
}
