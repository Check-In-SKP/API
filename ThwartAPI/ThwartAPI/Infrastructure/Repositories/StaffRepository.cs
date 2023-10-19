using ThwartAPI.Domain.Aggregates.StaffAggregate;
using ThwartAPI.Domain.Interfaces.Repositories;
using ThwartAPI.Infrastructure.Data;

namespace ThwartAPI.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _context;
        public StaffRepository(ApplicationDbContext context)
        {
               _context = context;
        }

        public Task AddAsync(Staff entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Staff>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Staff> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Staff entity)
        {
            throw new NotImplementedException();
        }
    }
}
