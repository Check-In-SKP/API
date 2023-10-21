using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.UserAggregate
{
    public class Token
    {
        private readonly int _id;
        public int Id => _id;

        [Required, StringLength(512)]
        public string JwtId { get; private set; }

        public bool IsRevoked { get; private set; }

        public DateTime ExpiryDate { get; private set; }

        // Constructor for new token
        internal Token(string jwtId, DateTime expiryDate)
        {
            ValidateInput(jwtId, expiryDate);

            JwtId = jwtId;
            ExpiryDate = expiryDate;
            IsRevoked = false;
        }

        // Constructor for existing token
        internal Token(int id, string jwtId, bool isRevoked, DateTime expiryDate)
        {
            ValidateInput(jwtId, expiryDate);

            _id = id;
            JwtId = jwtId;
            IsRevoked = isRevoked;
            ExpiryDate = expiryDate;
        }

        private void ValidateInput(string jwtId, DateTime expiryDate)
        {
            if (string.IsNullOrWhiteSpace(jwtId) || jwtId.Length > 512)
                throw new ArgumentException("Invalid JwtId.", nameof(jwtId));
            if (expiryDate <= DateTime.UtcNow)
                throw new ArgumentException("Invalid expiry date.", nameof(expiryDate));
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
