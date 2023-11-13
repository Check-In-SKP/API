using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Repositories;
using CheckInSKP.Infrastructure.Data;
using CheckInSKP.Infrastructure.Entities;
using CheckInSKP.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CheckInSKP.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserMapper _userMapper;

        public UserRepository(ApplicationDbContext context, UserMapper userMapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
        }

        public async Task<User?> AddAsync(User user)
        {
            var entity = _userMapper.MapToEntity(user);
            var addedEntity = await _context.AddAsync(entity);
            return _userMapper.MapToDomain(addedEntity.Entity);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var entity = await _context.Set<UserEntity>().FirstOrDefaultAsync(e => e.Id == id);

            return _userMapper.MapToDomain(entity);
        }


        public async Task UpdateAsync(User user)
        {
            var entity = await _context.Set<UserEntity>().FindAsync(user.Id);
            if (entity != null)
            {
                entity.Name = user.Name;
                entity.Username = user.Username;
                entity.PasswordHash = user.PasswordHash;
                entity.RoleId = user.RoleId;
            }
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _context.Set<UserEntity>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<UserEntity>().Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<UserEntity>().FindAsync(id) != null;
        }

        public async Task<IEnumerable<User?>> GetAllAsync()
        {
            var entities = await _context.Set<UserEntity>().ToListAsync();
            return entities.Select(_userMapper.MapToDomain);
        }

        public async Task<IEnumerable<User?>> GetWithPaginationAsync(int page, int pageSize)
        {
            var entities = await _context.Set<UserEntity>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return entities.Select(_userMapper.MapToDomain);
        }

        public async Task AddRangeAsync(IEnumerable<User> users)
        {
            var entities = users.Select(_userMapper.MapToEntity).ToList();
            await _context.Users.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<int> userIds)
        {
            var entities = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
            if(entities != null)
            {
                _context.Users.RemoveRange(entities);
            }
        }

        public IQueryable<User?> Query() => _context.Set<UserEntity>().Select(e => _userMapper.MapToDomain(e));

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var entity = await _context.Set<UserEntity>().FirstOrDefaultAsync(e => e.Username == username);
            return _userMapper.MapToDomain(entity);
        }
    }
}
