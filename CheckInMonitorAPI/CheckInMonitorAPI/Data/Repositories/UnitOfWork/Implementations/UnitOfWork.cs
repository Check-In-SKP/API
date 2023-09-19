using CheckInMonitorAPI.Data.Repositories.Implementations;
using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;

namespace CheckInMonitorAPI.Data.Repositories.UnitOfWork.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        // Flag to track whether the object has been disposed of
        private bool _disposed = false;

        // An instance of DatabaseContext which interacts with the database
        private readonly DatabaseContext _context;

        // Dictionary to hold repository instances; keys are types, values are repository instances
        private readonly Dictionary<Type, object> _repositories = new();

        /// <summary>
        /// Constructor that initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">An instance of DatabaseContext to be used by this UnitOfWork.</param>
        /// <exception cref="ArgumentNullException">Thrown when context is null.</exception>
        public UnitOfWork(DatabaseContext context)
        {
            // If the context is null, throw an ArgumentNullException, else assign it to _context
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the repository for the specified entity type T with key type TKey.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the entity's key.</typeparam>
        /// <returns>An instance of IGenericRepository for the specified type.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a repository instance could not be created.</exception>
        public IGenericRepository<T, TKey> GetRepository<T, TKey>() where T : class
        {
            var type = typeof(T);
            // If repository of type T does not already exist in the dictionary, create a new instance and add it to the dictionary
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TKey)), _context);

                if (repositoryInstance == null)
                {
                    throw new InvalidOperationException($"Could not create instance of repository for type {typeof(T).Name}");
                }

                _repositories[type] = repositoryInstance;
            }
            // Return the repository instance from the dictionary
            return (IGenericRepository<T, TKey>)_repositories[type];
        }

        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the database.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task CompleteAsync()
        {
            try
            {
                // Save all changes to the database asynchronously
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // TODO: Handle exception - currently this catch block swallows the exception silently
                // throw new UnitOfWorkException("An error occurred while saving changes.", ex);
            }
        }

        /// <summary>
        /// Releases the resources used by this UnitOfWork instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // Suppress finalization to prevent the finalizer from running
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by this UnitOfWork instance and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // If the object has already been disposed, return to prevent multiple disposal
            if (_disposed)
                return;

            // If disposing is true, release all managed resources
            if (disposing)
            {
                _context.Dispose();
            }

            // Mark the object as disposed
            _disposed = true;
        }

        /// <summary>
        /// Finalizer that releases unmanaged resources used by this UnitOfWork instance.
        /// </summary>
        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
