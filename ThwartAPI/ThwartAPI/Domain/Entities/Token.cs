using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThwartAPI.Domain.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public string JwtId { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpiryDate { get; set; }
        public User User { get; set; }

        public Token(string jwtId, DateTime expiryDate, User user)
        {
            JwtId = jwtId ?? throw new ArgumentNullException(nameof(jwtId));
            ExpiryDate = expiryDate;
            User = user ?? throw new ArgumentNullException(nameof(user));
            IsRevoked = false;
        }

        public void Revoke()
        {
            if (IsExpired())
            {
                throw new InvalidOperationException("Cannot revoke an expired token.");
            }
            IsRevoked = true;
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow > ExpiryDate;
        }
    }
}
