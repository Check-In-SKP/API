using Microsoft.EntityFrameworkCore;
using CheckInSKP.Domain.Entities;
using CheckInSKP.Infrastructure.Data;
using CheckInSKP.Infrastructure.Mappings;
using CheckInSKP.Infrastructure.Entities;
using CheckInSKP.Domain.Repositories;

namespace CheckInSKP.Infrastructure.Repositories
{
    public class TimeTypeRepository : ITimeTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly TimeTypeMapper _timeTypeMapper;

        public TimeTypeRepository(ApplicationDbContext context, TimeTypeMapper timeTypeMapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _timeTypeMapper = timeTypeMapper ?? throw new ArgumentNullException(nameof(timeTypeMapper));
        }

        public async Task AddAsync(TimeType timeType)
        {
            var entity = _timeTypeMapper.MapToEntity(timeType);
            _ = await _context.TimeTypes.AddAsync(entity);
        }

        public async Task<TimeType?> GetByIdAsync(int id)
        {
            var entity = await _context.TimeTypes.FindAsync(id);
            return _timeTypeMapper.MapToDomain(entity);
        }


        public async Task UpdateAsync(TimeType timeType)
        {
            var entity = await _context.Set<TimeTypeEntity>().FindAsync(timeType.Id);
            if(entity != null)
            {
                entity.Name = timeType.Name;
            }
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _context.Set<TimeTypeEntity>().FindAsync(id);
            if(entity != null)
                _context.Set<TimeTypeEntity>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<TimeTypeEntity>().FindAsync(id) != null;
        }

        public async Task<IEnumerable<TimeType?>> GetAllAsync()
        {
            var entities = await _context.Set<TimeTypeEntity>().ToListAsync();
            return entities.Select(_timeTypeMapper.MapToDomain);
        }

        public async Task<IEnumerable<TimeType?>> GetWithPaginationAsync(int page, int pageSize)
        {
            var entities = await _context.Set<TimeTypeEntity>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return entities.Select(_timeTypeMapper.MapToDomain);
        }

        public async Task AddRangeAsync(IEnumerable<TimeType> timeTypes)
        {
            var entities = timeTypes.Select(_timeTypeMapper.MapToEntity).ToList();
            await _context.TimeTypes.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<int> timeTypeIds)
        {
            var entities = await _context.TimeTypes.Where(u => timeTypeIds.Contains(u.Id)).ToListAsync();
            if(entities != null)
            {
                _context.TimeTypes.RemoveRange(entities);
            }
        }

        public IQueryable<TimeType?> Query() => _context.Set<TimeTypeEntity>().Select(e => _timeTypeMapper.MapToDomain(e));
    }
}
