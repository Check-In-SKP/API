using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Entities.StaffAggregate;
using System.Reflection.Metadata.Ecma335;

namespace CheckInSKP.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}
