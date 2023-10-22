using CheckInSKP.Domain.Entities.UserAggregate;

namespace CheckInSKP.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
    }
}
