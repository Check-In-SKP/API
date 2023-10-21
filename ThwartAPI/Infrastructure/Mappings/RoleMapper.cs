using System.Reflection;
using Domain.Entities;
using Domain.Factories;
using Infrastructure.Data.Entities;
using Infrastructure.Mappings.Interfaces;

namespace Infrastructure.Mappings
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
