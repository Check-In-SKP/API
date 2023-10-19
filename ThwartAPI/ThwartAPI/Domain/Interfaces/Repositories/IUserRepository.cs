using ThwartAPI.Domain.Aggregates.UserAggregate;

namespace ThwartAPI.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
    }
}
