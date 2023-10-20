using Microsoft.EntityFrameworkCore;
using ThwartAPI.Domain.Entities.UserAggregate;
using ThwartAPI.Domain.Interfaces.Repositories;
using ThwartAPI.Infrastructure.Data;
using ThwartAPI.Infrastructure.Data.Entities;
using ThwartAPI.Infrastructure.Exceptions;
using ThwartAPI.Infrastructure.Mappings;

namespace ThwartAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserMap _userMap;

        public UserRepository(ApplicationDbContext context, UserMap userMap)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userMap = userMap ?? throw new ArgumentNullException(nameof(userMap));
        }

        public async Task AddAsync(User user)
        {
            UserEntity entity = _userMap.MapToEntity(user);
            await _context.Users.AddAsync(entity);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            UserEntity? entity = await _context.Set<UserEntity>()
                                              .Include(e => e.Tokens)
                                              .FirstOrDefaultAsync(e => e.Id == id);

            return entity == null ? throw new EntityNotFoundException($"User with id {id} not found.") : _userMap.MapToDomain(entity);
        }


        public async Task UpdateAsync(User user)
        {
            UserEntity entity = await _context.Set<UserEntity>().FindAsync(user.Id) ?? throw new EntityNotFoundException($"User with id {user.Id} not found.");
            
            entity = _userMap.MapToEntity(user);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task RemoveAsync(int id)
        {
            UserEntity? entity = await _context.Set<UserEntity>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<UserEntity>().Remove(entity);
            }
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Set<UserEntity>().AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            List<UserEntity> entities = await _context.Set<UserEntity>().Include(e => e.Tokens).ToListAsync();
            return entities.Select(e => _userMap.MapToDomain(e));
        }

        public Task AddRangeAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

    }
}
