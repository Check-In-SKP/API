using System.Reflection;
using ThwartAPI.Domain.Entities;
using ThwartAPI.Domain.Factories;
using ThwartAPI.Infrastructure.Data.Entities;
using ThwartAPI.Infrastructure.Mappings.Interfaces;

namespace ThwartAPI.Infrastructure.Mappings
{
    public class RoleMapper : IGenericMapper<Role, RoleEntity>
    {
        private readonly RoleFactory _roleFactory;
        public RoleMapper(RoleFactory roleFactory)
        {
            _roleFactory = roleFactory ?? throw new ArgumentNullException(nameof(roleFactory));
        }

        public Role MapToDomain(RoleEntity entity)
        {
            return _roleFactory.CreateRole(entity.Id, entity.Name);
        }

        public RoleEntity MapToEntity(Role domain)
        {
            return new RoleEntity
            {
                Id = domain.Id,
                Name = domain.Name
            };
        }
    }
}
