using ThwartAPI.Domain.Entities;
using ThwartAPI.Domain.Interfaces.Repositories;

namespace ThwartAPI.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Task AddAsync(Role entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Role> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<Role> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
