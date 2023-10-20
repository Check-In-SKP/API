using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using ThwartAPI.Domain.Entities;

namespace ThwartAPI.Domain.Entities.UserAggregate
{
    public class User
    {
        private readonly int _id;
        public int Id => _id;

        [Required, StringLength(64)]
        public string Name { get; private set; }

        [Required, StringLength(128)]
        public string Username { get; private set; }

        [Required, StringLength(128)]
        public string PasswordHash { get; private set; }

        public Role Role { get; private set; }

        private ImmutableList<Token> _tokens = ImmutableList<Token>.Empty;
        public IReadOnlyList<Token> Tokens => _tokens;

        // Constructor for new User
        public User(string name, string username, string passwordHash, Role role)
        {
            ValidateInput(name, username, passwordHash, role);

            Name = name;
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }

        // Constructor for existing User
        public User(int id, string name, string username, string passwordHash, Role role, IEnumerable<Token>? tokens = null)
        {
            ValidateInput(name, username, passwordHash, role);

            _id = id;
            Name = name;
            Username = username;
            PasswordHash = passwordHash;
            Role = role;

            if (tokens != null)
            {
                foreach (var token in tokens)
                {
                    AddToken(token);
                }
            }
        }

        private void ValidateInput(string name, string username, string passwordHash, Role role)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 64)
                throw new ArgumentException("Invalid name.", nameof(name));
            if (string.IsNullOrWhiteSpace(username) || username.Length > 128)
                throw new ArgumentException("Invalid username.", nameof(username));
            if (string.IsNullOrWhiteSpace(passwordHash) || passwordHash.Length > 128)
                throw new ArgumentException("Invalid password hash.", nameof(passwordHash));
            if (role == null)
                throw new ArgumentNullException(nameof(role));
        }

        public void AddToken(Token token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            _tokens = _tokens.Add(token);
        }

        public void RemoveToken(Token token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            _tokens = _tokens.Remove(token);
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName) || newName.Length > 64)
                throw new ArgumentException("Invalid new user name.", nameof(newName));
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
