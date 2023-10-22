﻿using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace CheckInSKP.Domain.Entities.UserAggregate
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

        public int RoleId { get; private set; }

        private ImmutableList<Token> _tokens = ImmutableList<Token>.Empty;
        public IReadOnlyList<Token> Tokens => _tokens;

        // Constructor for new User
        public User(string name, string username, string passwordHash, int roleId)
        {
            ValidateInput(name, username, passwordHash, roleId);

            Name = name;
            Username = username;
            PasswordHash = passwordHash;
            RoleId = roleId;
        }

        // Constructor for existing User
        public User(int id, string name, string username, string passwordHash, int roleId, IEnumerable<Token>? tokens = null)
        {
            ValidateInput(name, username, passwordHash, roleId);

            _id = id;
            Name = name;
            Username = username;
            PasswordHash = passwordHash;
            RoleId = roleId;

            if (tokens != null)
            {
                foreach (var token in tokens)
                {
                    AddToken(token);
                }
            }
        }

        private void ValidateInput(string name, string username, string passwordHash, int roleId)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 64)
                throw new ArgumentException("Invalid name.", nameof(name));
            if (string.IsNullOrWhiteSpace(username) || username.Length > 128)
                throw new ArgumentException("Invalid username.", nameof(username));
            if (string.IsNullOrWhiteSpace(passwordHash) || passwordHash.Length > 128)
                throw new ArgumentException("Invalid password hash.", nameof(passwordHash));
            if (roleId <= 0)
                throw new ArgumentException("Invalid role ID.", nameof(roleId));
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

        public void UpdateRole(int newRoleId)
        {
            if (newRoleId <= 0)
            {
                throw new ArgumentException("Invalid new role ID.");
            }

            RoleId = newRoleId;
        }
    }
}