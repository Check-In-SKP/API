using ThwartAPI.Domain.Entities.UserAggregate;
using ThwartAPI.Domain.Entities;

namespace ThwartAPI.Domain.Factories
{
    public class UserFactory
    {
        public User CreateUser(int id, string name, string username, string passwordHash, Role role, IEnumerable<Token>? tokens = null)
        {
            return new User(id, name, username, passwordHash, role, tokens);
        }

        public User CreateNewUser(string name, string username, string passwordHash, Role role)
        {
            return new User(name, username, passwordHash, role);
        }

        public Token CreateToken(int id, string jwtId, bool isRevoked, DateTime expiryDate)
        {
            return new Token(id, jwtId, isRevoked, expiryDate);
        }

        public Token CreateNewToken(string jwtId, DateTime expiryDate)
        {
            return new Token(jwtId, expiryDate);
        }
    }
}
