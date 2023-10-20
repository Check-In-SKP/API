using ThwartAPI.Domain.Entities;
using ThwartAPI.Domain.Interfaces.Repositories;

namespace ThwartAPI.Infrastructure.Repositories
{
    public class TimeTypeRepository : ITimeTypeRepository
    {
        public Task AddAsync(TimeType entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<TimeType> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TimeType>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TimeType> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<TimeType> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TimeType entity)
        {
            throw new NotImplementedException();
        }
    }
}
