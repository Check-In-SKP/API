using Microsoft.AspNetCore.Http.HttpResults;
using ThwartAPI.Domain.Entities;

namespace ThwartAPI.Domain.Aggregates.UserAggregate
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public Role Role { get; private set; }
        public ICollection<Token> Tokens { get; private set; } = new List<Token>();

        public User(string name, string username, string passwordHash, Role role)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Role = role ?? throw new ArgumentNullException(nameof(role));
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrEmpty(newName) || newName.Length > 64)
            {
                throw new ArgumentException("Invalid new user name.");
            }

            Name = newName;
        }

        public void UpdateUsername(string newUsername)
        {
            if (string.IsNullOrEmpty(newUsername) || newUsername.Length > 128)
            {
                throw new ArgumentException("Invalid new username.");
            }

            Username = newUsername;
        }

        public void UpdatePasswordHash(string newPasswordHash)
        {
            if (string.IsNullOrEmpty(newPasswordHash) || newPasswordHash.Length > 128)
            {
                throw new ArgumentException("Invalid new password hash.");
            }

            PasswordHash = newPasswordHash;
        }

        public void UpdateRole(Role newRole)
        {

            Role = newRole ?? throw new ArgumentNullException(nameof(newRole));
        }
    }
}
