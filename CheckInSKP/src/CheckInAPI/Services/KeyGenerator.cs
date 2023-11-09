using System;
using System.Security.Cryptography;

namespace API.Services
{
    public class KeyGenerator
    {
        public static string GenerateRandomKey(int length) => Convert.ToBase64String(RandomNumberGenerator.GetBytes(length));
    }
}
