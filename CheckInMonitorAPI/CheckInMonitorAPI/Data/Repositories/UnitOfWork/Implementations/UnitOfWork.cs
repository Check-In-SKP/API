using CheckInMonitorAPI.Data.Context;
using CheckInMonitorAPI.Data.Repositories.Implementations;
using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;
using CheckInMonitorAPI.Exceptions.Data;

namespace CheckInMonitorAPI.Data.Repositories.UnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        private readonly DatabaseContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly ILogger<UnitOfWork> _logger;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(DatabaseContext context, ILogger<UnitOfWork> logger, IServiceProvider serviceProvider)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider;
        }

        public IGenericRepository<T, TKey> GetRepository<T, TKey>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<,>);
                var logger = _serviceProvider.GetService<ILogger<GenericRepository<T, TKey>>>();
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TKey)), _context, logger);

                if (repositoryInstance == null)
                {
                    throw new InvalidOperationException($"Could not create instance of repository for type {typeof(T).Name}");
                }

                _repositories[type] = repositoryInstance;
            }
            return (IGenericRepository<T, TKey>)_repositories[type];
        }

        public async Task CompleteAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new UnitOfWorkException("An error occurred while saving changes.", ex);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            //if (disposing)
            //{
            //    _context.Dispose();
            //}

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
