using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Factories;
using CheckInSKP.Infrastructure.Entities;
using CheckInSKP.Infrastructure.Mappings.Interfaces;

namespace CheckInSKP.Infrastructure.Mappings
{
    public class RoleMapper : IGenericMapper<Role, RoleEntity>
    {
        private readonly RoleFactory _roleFactory;
        public RoleMapper(RoleFactory roleFactory)
        {
            _roleFactory = roleFactory ?? throw new ArgumentNullException(nameof(roleFactory));
        }

        public Role? MapToDomain(RoleEntity? entity)
        {
            if (entity == null)
                return null;

            return _roleFactory.CreateRole(entity.Id, entity.Name);
        }

        public RoleEntity MapToEntity(Role domain)
        {
            // Throws an null exceptions under the extreme circumstance that the domain is null.
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            return new RoleEntity
            {
                Id = domain.Id,
                Name = domain.Name
            };
        }
    }
}
