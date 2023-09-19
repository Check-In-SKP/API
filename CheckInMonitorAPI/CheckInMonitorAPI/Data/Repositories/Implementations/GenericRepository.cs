using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Exceptions.Data;
using Microsoft.EntityFrameworkCore;

namespace CheckInMonitorAPI.Data.Repositories.Implementations
{
    /// <summary>
    /// A generic repository class for managing entities of type T with a primary key of type TKey.
    /// </summary>
    /// <typeparam name="T">The type of the entity to be managed by this repository.</typeparam>
    /// <typeparam name="TKey">The type of the primary key of the entity.</typeparam>
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        // The DatabaseContext instance to interact with the database
        private readonly DatabaseContext _context;

        // The DbSet instance representing the collection of all entities in the context
        private readonly DbSet<T> _dbSet;

        // The logger instance to log information and warnings
        private readonly ILogger<GenericRepository<T, TKey>> _logger;

        /// <summary>
        /// Initializes a new instance of the GenericRepository class.
        /// </summary>
        /// <param name="context">An instance of DatabaseContext to be used by this repository.</param>
        /// <param name="logger">An instance of ILogger to be used for logging in this repository.</param>
        /// <exception cref="ArgumentNullException">Thrown when context or logger is null.</exception>
        public GenericRepository(DatabaseContext context, ILogger<GenericRepository<T, TKey>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Asynchronously retrieves all entities.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation, with a return value of IEnumerable of T.</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A Task representing the asynchronous operation, with a return value of the entity of type T.</returns>
        /// <exception cref="EntityNotFoundException">Thrown when the entity with the specified ID does not exist.</exception>
        public async Task<T> GetByIdAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning($"Entity with id '{id}' not found.");
                throw new EntityNotFoundException($"Entity with id '{id}' not found.");
            }
            return entity;
        }

        /// <summary>
        /// Asynchronously adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the entity is null.</exception>
        public async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Asynchronously updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the entity is null.</exception>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Asynchronously deletes an existing entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the entity is null.</exception>
        public async Task DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Asynchronously adds a range of entities.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the entities collection is null or empty.</exception>
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException(nameof(entities));

            await _dbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// Asynchronously deletes a range of entities.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the entities collection is null or empty.</exception>
        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException(nameof(entities));

            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Asynchronously checks if an entity with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the entity to check.</param>
        /// <returns>A Task representing the asynchronous operation, with a return value indicating whether the entity exists.</returns>
        public async Task<bool> ExistsAsync(TKey id)
        {
            return await _dbSet.FindAsync(id) != null;
        }

        /// <summary>
        /// Gets a queryable interface to the collection of entities of type T.
        /// </summary>
        /// <returns>An IQueryable of T representing the collection of entities.</returns>
        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }
    }
}
