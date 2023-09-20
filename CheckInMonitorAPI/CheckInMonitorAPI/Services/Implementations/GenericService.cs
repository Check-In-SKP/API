using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;
using CheckInMonitorAPI.Services.Interfaces;

namespace CheckInMonitorAPI.Services.Implementations
{
    public class GenericService<T, TKey> : IGenericService<T, TKey> where T : class
    {
        private readonly IGenericRepository<T, TKey> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GenericService<T, TKey>> _logger;

        public GenericService(IUnitOfWork unitOfWork, ILogger<GenericService<T, TKey>> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = unitOfWork.GetRepository<T, TKey>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public bool EntityExist(TKey id)
        {
            if(GetByIdAsync(id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _repository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _repository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(TKey id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException(nameof(entities));

            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException(nameof(entities));

            await _repository.DeleteRangeAsync(entities);
            await _unitOfWork.CompleteAsync();
        }

        public IQueryable<T> Query()
        {
            return _repository.Query();
        }
    }
}
