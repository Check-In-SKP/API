using Microsoft.EntityFrameworkCore;
using CheckInSKP.Domain.Interfaces.Repositories;
using CheckInSKP.Domain.Entities;
using CheckInSKP.Infrastructure.Data;
using CheckInSKP.Infrastructure.Mappings;
using CheckInSKP.Infrastructure.Exceptions;
using CheckInSKP.Infrastructure.Entities;

namespace CheckInSKP.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleMapper _roleMapper;

        public RoleRepository(ApplicationDbContext context, RoleMapper roleMapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _roleMapper = roleMapper ?? throw new ArgumentNullException(nameof(roleMapper));
        }

        public async Task AddAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            RoleEntity entity = _roleMapper.MapToEntity(role);
            await _context.Roles.AddAsync(entity);
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            RoleEntity entity = await _context.Set<RoleEntity>()
                                               .FirstOrDefaultAsync(e => e.Id == id) ?? throw new EntityNotFoundException($"Role with id {id} not found.");

            return _roleMapper.MapToDomain(entity);
        }


        public async Task UpdateAsync(Role role)
        {
            RoleEntity entity = await _context.Set<RoleEntity>().FindAsync(role.Id) ?? throw new EntityNotFoundException($"Role with id {role.Id} not found.");

            entity = _roleMapper.MapToEntity(role);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task RemoveAsync(int id)
        {
            RoleEntity entity = await _context.Set<RoleEntity>().FindAsync(id) ?? throw new EntityNotFoundException($"Role with id {id} not found.");

            _context.Set<RoleEntity>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<RoleEntity>().FindAsync(id) != null ? true : false;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            List<RoleEntity> entities = await _context.Set<RoleEntity>().ToListAsync() ?? throw new EntityNotFoundException("No roles found.");
            return entities.Select(e => _roleMapper.MapToDomain(e));
        }

        public async Task AddRangeAsync(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
            {
                if (role == null)
                {
                    throw new ArgumentNullException(nameof(role));
                }
            }

            List<RoleEntity> entities = roles.Select(_roleMapper.MapToEntity).ToList();
            await _context.Roles.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<int> roleIds)
        {
            List<RoleEntity> entities = await _context.Roles.Where(u => roleIds.Contains(u.Id)).ToListAsync() ?? throw new EntityNotFoundException("No roles found.");
            _context.Roles.RemoveRange(entities);
        }
    }
}
