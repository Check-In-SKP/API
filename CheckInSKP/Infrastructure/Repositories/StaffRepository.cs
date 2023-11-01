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

        public async Task AddAsync(Staff staff)
        {
            var entity = _staffMapper.MapToEntity(staff);
            _ = await _context.Staffs.AddAsync(entity);
        }

        public async Task<Staff?> GetByIdAsync(int id)
        {
            var entity = await _context.Set<StaffEntity>()
                                               .Include(e => e.TimeLogs)
                                               .FirstOrDefaultAsync(e => e.Id == id);

            return _staffMapper.MapToDomain(entity);
        }


        public async Task UpdateAsync(Staff staff)
        {
            var entity = await _context.Set<StaffEntity>().FindAsync(staff.Id);

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

        public async Task RemoveAsync(int id)
        {
            var entity = await _context.Set<StaffEntity>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<StaffEntity>().Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<StaffEntity>().FindAsync(id) != null;
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

        public async Task RemoveRangeAsync(IEnumerable<int> staffIds)
        {
            var entities = await _context.Staffs.Where(u => staffIds.Contains(u.Id)).ToListAsync();
            if(entities != null)
            {
                _context.Staffs.RemoveRange(entities);
            }
        }

        public IQueryable<Staff?> Query() => _context.Set<StaffEntity>().Select(e => _staffMapper.MapToDomain(e));

        public async Task UpdateTimeLogAsync(int staffId, TimeLog updatedTimeLog)
        {
            var entity = await _context.Set<StaffEntity>().Include(e => e.TimeLogs).FirstOrDefaultAsync(e => e.Id == staffId);

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
            var entity = await _context.Set<StaffEntity>().Include(e => e.TimeLogs).FirstOrDefaultAsync(e => e.Id == staffId);

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
            var entity = await _context.Set<StaffEntity>().Include(e => e.TimeLogs).FirstOrDefaultAsync(e => e.Id == staffId);

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
            var entity = await _context.Set<StaffEntity>().Include(e => e.TimeLogs).Skip((page - 1) * pageSize).Take(pageSize).FirstOrDefaultAsync(e => e.Id == staffId);
            return _staffMapper.MapToDomain(entity);
        }
    }
}
