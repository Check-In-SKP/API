using CheckInSKP.Domain.Entities.UserAggregate;
using CheckInSKP.Domain.Interfaces.Repositories;
using CheckInSKP.Infrastructure.Data;
using CheckInSKP.Infrastructure.Entities;
using CheckInSKP.Infrastructure.Exceptions;
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

        public async Task AddAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            UserEntity entity = _userMapper.MapToEntity(user);
            await _context.Users.AddAsync(entity);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            UserEntity entity = await _context.Set<UserEntity>()
                                               .Include(e => e.Tokens)
                                               .FirstOrDefaultAsync(e => e.Id == id) ?? throw new EntityNotFoundException($"User with id {id} not found.");

            return _userMapper.MapToDomain(entity);
        }


        public async Task UpdateAsync(User user)
        {
            UserEntity entity = await _context.Set<UserEntity>().FindAsync(user.Id) ?? throw new EntityNotFoundException($"User with id {user.Id} not found.");

            entity = _userMapper.MapToEntity(user);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task RemoveAsync(int id)
        {
            UserEntity entity = await _context.Set<UserEntity>().FindAsync(id) ?? throw new EntityNotFoundException($"User with id {id} not found.");

            _context.Set<UserEntity>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<UserEntity>().FindAsync(id) != null ? true : false;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            List<UserEntity> entities = await _context.Set<UserEntity>().Include(e => e.Tokens).ToListAsync() ?? throw new EntityNotFoundException("No users found.");
            return entities.Select(e => _userMapper.MapToDomain(e));
        }

        public async Task AddRangeAsync(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
            }

            List<UserEntity> entities = users.Select(_userMapper.MapToEntity).ToList();
            await _context.Users.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<int> userIds)
        {
            List<UserEntity> entities = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync() ?? throw new EntityNotFoundException("No users found.");
            _context.Users.RemoveRange(entities);
        }
    }
}
