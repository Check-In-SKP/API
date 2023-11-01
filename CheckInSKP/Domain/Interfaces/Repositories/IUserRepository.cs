using CheckInSKP.Domain.Entities.StaffAggregate;
using CheckInSKP.Domain.Entities.UserAggregate;
using System.Reflection.Metadata.Ecma335;

namespace CheckInSKP.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task UpdateTokenAsync(int userId, Token updatedToken);
        Task RemoveTokenAsync(int userId, int tokenId);
        Task AddTokenAsync(int userId, Token token);
        Task<User?> GetUserWithPagedTokensAsync(int userId, int page, int pageSize);
    }
}
