using Domain.Entities.UserAggregate;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
    }
}
