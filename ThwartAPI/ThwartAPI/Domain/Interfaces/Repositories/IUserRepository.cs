using ThwartAPI.Domain.Entities.UserAggregate;

namespace ThwartAPI.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
    }
}
