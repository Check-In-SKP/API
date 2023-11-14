using CheckInSKP.Domain.Entities;

namespace CheckInSKP.Domain.Factories
{
    public class UserFactory
    {
        public User CreateUser(Guid id, string name, string username, string passwordHash, int roleId)
        {
            return new User(id, name, username, passwordHash, roleId);
        }

        public User CreateNewUser(string name, string username, string passwordHash, int roleId)
        {
            return new User(name, username, passwordHash, roleId);
        }
    }
}
