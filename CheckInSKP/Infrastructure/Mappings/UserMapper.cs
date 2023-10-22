using CheckInSKP.Domain.Entities.UserAggregate;
using CheckInSKP.Domain.Factories;
using CheckInSKP.Infrastructure.Data.Entities;
using CheckInSKP.Infrastructure.Mappings.Interfaces;

namespace CheckInSKP.Infrastructure.Mappings
{
    public class UserMapper : IGenericMapper<User, UserEntity>
    {
        private readonly UserFactory _userFactory;

        public UserMapper(UserFactory userFactory)
        {
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
        }

        public User MapToDomain(UserEntity entity)
        {
            // Maps token entities from user to domain tokens
            var tokens = entity.Tokens.Select(MapTokenToDomain).ToList();

            return _userFactory.CreateUser(entity.Id, entity.Name, entity.Username, entity.PasswordHash, entity.RoleId, tokens);
        }

        public UserEntity MapToEntity(User domain)
        {

            // Maps tokens from user to entity tokens
            var tokens = domain.Tokens.Select(MapTokenToEntity).ToList();

            return new UserEntity
            {
                Id = domain.Id,
                Name = domain.Name,
                Username = domain.Username,
                PasswordHash = domain.PasswordHash,
                RoleId = domain.RoleId,
                Tokens = tokens
            };
        }

        private Token MapTokenToDomain(TokenEntity tokenEntity)
        {
            return _userFactory.CreateToken(tokenEntity.Id, tokenEntity.JwtId, tokenEntity.IsRevoked, tokenEntity.ExpiryDate);
        }

        private TokenEntity MapTokenToEntity(Token token)
        {
            return new TokenEntity
            {
                Id = token.Id,
                JwtId = token.JwtId,
                IsRevoked = token.IsRevoked,
                ExpiryDate = token.ExpiryDate
            };
        }
    }
}
