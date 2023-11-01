using Microsoft.EntityFrameworkCore;
using CheckInSKP.Domain.Entities;
using CheckInSKP.Infrastructure.Data;
using CheckInSKP.Infrastructure.Mappings;
using CheckInSKP.Infrastructure.Entities;
using CheckInSKP.Domain.Repositories;

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
            var entity = _roleMapper.MapToEntity(role);
            _ = await _context.Roles.AddAsync(entity);
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            var entity = await _context.Set<RoleEntity>()
                                               .FirstOrDefaultAsync(e => e.Id == id);

            return _roleMapper.MapToDomain(entity);
        }

        public async Task UpdateAsync(Role role)
        {
            var entity = await _context.Set<RoleEntity>().FindAsync(role.Id);
            if(entity != null)
            {
                entity.Name = role.Name;
            }
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _context.Set<RoleEntity>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<RoleEntity>().Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<RoleEntity>().FindAsync(id) != null;
        }

        public async Task<IEnumerable<Role?>> GetAllAsync()
        {
            var entities = await _context.Set<RoleEntity>().ToListAsync();
            return entities.Select(_roleMapper.MapToDomain);
        }

        public async Task<IEnumerable<Role?>> GetWithPaginationAsync(int page, int pageSize)
        {
            var entities = await _context.Set<RoleEntity>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return entities.Select(_roleMapper.MapToDomain);
        }

        public async Task AddRangeAsync(IEnumerable<Role> roles)
        {
            var entities = roles.Select(_roleMapper.MapToEntity).ToList();
            await _context.Roles.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<int> roleIds)
        {
            var entities = await _context.Roles.Where(u => roleIds.Contains(u.Id)).ToListAsync();
            if(entities != null)
            {
                _context.Roles.RemoveRange(entities);
            }
        }

        public IQueryable<Role?> Query() => _context.Set<RoleEntity>().Select(e => _roleMapper.MapToDomain(e));
    }
}
