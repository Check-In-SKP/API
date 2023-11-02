using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Factories;
using CheckInSKP.Infrastructure.Entities;
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

        public User? MapToDomain(UserEntity? entity)
        {
            if(entity == null)
                return null;

            return _userFactory.CreateUser(entity.Id, entity.Name, entity.Username, entity.PasswordHash, entity.RoleId);
        }

        public UserEntity MapToEntity(User domain)
        {
            // Throws an null exceptions under the extreme circumstance that the domain is null.
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            return new UserEntity
            {
                Id = domain.Id,
                Name = domain.Name,
                Username = domain.Username,
                PasswordHash = domain.PasswordHash,
                RoleId = domain.RoleId,
            };
        }
    }
}
