using ThwartAPI.Domain.Entities;
using ThwartAPI.Domain.Interfaces.Repositories;

namespace ThwartAPI.Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        public Task AddAsync(Device entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Device> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Device>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Device> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<Device> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Device entity)
        {
            throw new NotImplementedException();
        }
    }
}
