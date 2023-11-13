using CheckInSKP.Domain.Entities.StaffAggregate;
using CheckInSKP.Domain.Repositories;
using CheckInSKP.Infrastructure.Data;
using CheckInSKP.Infrastructure.Entities;
using CheckInSKP.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CheckInSKP.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly StaffMapper _staffMapper;

        public StaffRepository(ApplicationDbContext context, StaffMapper staffMapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _staffMapper = staffMapper ?? throw new ArgumentNullException(nameof(staffMapper));
        }

        public async Task<Staff?> AddAsync(Staff staff)
        {
            var entity = _staffMapper.MapToEntity(staff);
            var addedEntity = await _context.AddAsync(entity);
            return _staffMapper.MapToDomain(addedEntity.Entity);
        }

        public async Task<Staff?> GetByIdAsync(int userId)
        {
            var entity = await _context.Set<StaffEntity>()
                                               .Include(e => e.TimeLogs)
                                               .FirstOrDefaultAsync(e => e.UserId == userId);

            return _staffMapper.MapToDomain(entity);
        }


        public async Task UpdateAsync(Staff staff)
        {
            var entity = await _context.Set<StaffEntity>().FindAsync(staff.UserId);

            if(entity != null)
            {
                entity.PhoneNumber = staff.PhoneNumber;
                entity.CardNumber = staff.CardNumber;
                entity.PhoneNotification = staff.PhoneNotification;
                entity.Preoccupied = staff.Preoccupied;
                entity.MeetingTime = staff.MeetingTime;
                entity.UserId = staff.UserId;
            }
        }

        public async Task RemoveAsync(int userId)
        {
            var entity = await _context.Set<StaffEntity>().FindAsync(userId);
            if(entity != null)
            {
                _context.Set<StaffEntity>().Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(int userId)
        {
            return await _context.Set<StaffEntity>().FindAsync(userId) != null;
        }

        public async Task<IEnumerable<Staff?>> GetAllAsync()
        {
            var entities = await _context.Set<StaffEntity>()
                                                       .Include(e => e.TimeLogs)
                                                       .ToListAsync();
            return entities.Select(_staffMapper.MapToDomain);
        }

        public async Task<IEnumerable<Staff?>> GetWithPaginationAsync(int page, int pageSize)
        {
            var entities = await _context.Set<StaffEntity>().Include(e => e.TimeLogs).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return entities.Select(_staffMapper.MapToDomain);
        }

        public async Task AddRangeAsync(IEnumerable<Staff> staffs)
        {
            var entities = staffs.Select(_staffMapper.MapToEntity).ToList();
            await _context.Staffs.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<int> userIds)
        {
            var entities = await _context.Staffs.Where(u => userIds.Contains(u.UserId)).ToListAsync();
            if(entities != null)
            {
                _context.Staffs.RemoveRange(entities);
            }
        }

        public IQueryable<Staff?> Query() => _context.Set<StaffEntity>().Select(e => _staffMapper.MapToDomain(e));

        public async Task UpdateTimeLogAsync(int staffId, TimeLog updatedTimeLog)
        {
            var entity = await _context.Set<StaffEntity>().Include(e => e.TimeLogs).FirstOrDefaultAsync(e => e.UserId == staffId);

            if(entity != null)
            {
                var timeLog = entity.TimeLogs.FirstOrDefault(t => t.Id == updatedTimeLog.Id);
                if(timeLog != null)
                {
                    timeLog.TimeTypeId = updatedTimeLog.TimeTypeId;
                    timeLog.TimeStamp = updatedTimeLog.TimeStamp;
                }
            }
        }

        public async Task RemoveTimeLogAsync(int staffId, int tokenId)
        {
            var entity = await _context.Set<StaffEntity>().Include(e => e.TimeLogs).FirstOrDefaultAsync(e => e.UserId == staffId);

            if(entity != null)
            {
                var timeLog = entity.TimeLogs.FirstOrDefault(t => t.Id == tokenId);
                if(timeLog != null)
                {
                    entity.TimeLogs.Remove(timeLog);
                }
            }
        }

        public async Task AddTimeLogAsync(int staffId, TimeLog timeLog)
        {
            var entity = await _context.Set<StaffEntity>().Include(e => e.TimeLogs).FirstOrDefaultAsync(e => e.UserId == staffId);

            if(entity != null)
            {
                entity.TimeLogs.Add(new TimeLogEntity
                {
                    TimeStamp = timeLog.TimeStamp,
                    TimeTypeId = timeLog.TimeTypeId
                });
            }
        }

        public async Task<Staff?> GetStaffWithPagedTimeLogs(int staffId, int page, int pageSize)
        {
            var entity = await _context.Set<StaffEntity>().Include(e => e.TimeLogs).Skip((page - 1) * pageSize).Take(pageSize).FirstOrDefaultAsync(e => e.UserId == staffId);
            return _staffMapper.MapToDomain(entity);
        }

        public async Task<Staff?> GetByCardNumberAsync(string cardNumber)
        {
            var entity = await _context.Set<StaffEntity>().Include(e => e.TimeLogs).FirstOrDefaultAsync(e => e.CardNumber == cardNumber);
            return _staffMapper.MapToDomain(entity);
        }

        // Gets staffs who aren't preoccupied with timelogs for today
        public async Task<IEnumerable<Staff?>> GetAvailableStaffsWithTodayTimeLogs()
        {
            var today = DateTime.Today;

            var entities = await _context.Set<StaffEntity>()
                                         .Where(e => !e.Preoccupied) // Filter out preoccupied staff
                                         .Include(e => e.TimeLogs.Where(t => t.TimeStamp >= today))
                                         .ToListAsync();

            return entities.Select(_staffMapper.MapToDomain).ToList();
        }
    }
}
