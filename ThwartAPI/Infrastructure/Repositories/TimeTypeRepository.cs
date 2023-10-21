using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Mappings;
using Infrastructure.Data.Entities;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Exceptions;

namespace Infrastructure.Repositories
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
            if (timeType == null)
            {
                throw new ArgumentNullException(nameof(timeType));
            }

            TimeTypeEntity entity = _timeTypeMapper.MapToEntity(timeType);
            await _context.TimeTypes.AddAsync(entity);
        }

        public async Task<TimeType> GetByIdAsync(int id)
        {
            TimeTypeEntity entity = await _context.Set<TimeTypeEntity>()
                                               .FirstOrDefaultAsync(e => e.Id == id) ?? throw new EntityNotFoundException($"TimeType with id {id} not found.");

            return _timeTypeMapper.MapToDomain(entity);
        }


        public async Task UpdateAsync(TimeType timeType)
        {
            TimeTypeEntity entity = await _context.Set<TimeTypeEntity>().FindAsync(timeType.Id) ?? throw new EntityNotFoundException($"TimeType with id {timeType.Id} not found.");

            entity = _timeTypeMapper.MapToEntity(timeType);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task RemoveAsync(int id)
        {
            TimeTypeEntity entity = await _context.Set<TimeTypeEntity>().FindAsync(id) ?? throw new EntityNotFoundException($"TimeType with id {id} not found.");

            _context.Set<TimeTypeEntity>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<TimeTypeEntity>().FindAsync(id) != null ? true : false;
        }

        public async Task<IEnumerable<TimeType>> GetAllAsync()
        {
            List<TimeTypeEntity> entities = await _context.Set<TimeTypeEntity>().ToListAsync() ?? throw new EntityNotFoundException("No TimeTypes found.");
            return entities.Select(e => _timeTypeMapper.MapToDomain(e));
        }

        public async Task AddRangeAsync(IEnumerable<TimeType> timeTypes)
        {
            foreach (var timeType in timeTypes)
            {
                if (timeType == null)
                {
                    throw new ArgumentNullException(nameof(timeType));
                }
            }

            List<TimeTypeEntity> entities = timeTypes.Select(_timeTypeMapper.MapToEntity).ToList();
            await _context.TimeTypes.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<TimeType> timeTypes)
        {
            IEnumerable<int> timeTypeIds = timeTypes.Select(u => u.Id);
            List<TimeTypeEntity> entities = await _context.TimeTypes.Where(u => timeTypeIds.Contains(u.Id)).ToListAsync() ?? throw new EntityNotFoundException("No TimeTypes found.");
            _context.TimeTypes.RemoveRange(entities);
        }
    }
}
