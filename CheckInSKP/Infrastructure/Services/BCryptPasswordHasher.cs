using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckInSKP.Application.Common.Interfaces;
using BCrypt.Net;

namespace CheckInSKP.Infrastructure.Services
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        // Determines the number of iterations used to hash passwords.
        // The default work factor is 10, which is fine for most applications.
        private const int _workFactor = 10;

        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, _workFactor);
        public bool VerifyPassword(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
