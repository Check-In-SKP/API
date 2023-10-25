using CheckInSKP.Domain.Entities.StaffAggregate;
using CheckInSKP.Domain.Interfaces.Repositories;
using CheckInSKP.Infrastructure.Data;
using CheckInSKP.Infrastructure.Entities;
using CheckInSKP.Infrastructure.Exceptions;
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
            if (staff == null)
            {
                throw new ArgumentNullException(nameof(staff));
            }

            StaffEntity entity = _staffMapper.MapToEntity(staff);
            await _context.Staffs.AddAsync(entity);
        }

        public async Task<Staff> GetByIdAsync(int id)
        {
            StaffEntity entity = await _context.Set<StaffEntity>()
                                               .Include(e => e.TimeLogs)
                                               .FirstOrDefaultAsync(e => e.Id == id) ?? throw new EntityNotFoundException($"Staff with id {id} not found.");

            return _staffMapper.MapToDomain(entity);
        }


        public async Task UpdateAsync(Staff staff)
        {
            StaffEntity entity = await _context.Set<StaffEntity>().FindAsync(staff.Id) ?? throw new EntityNotFoundException($"Staff with id {staff.Id} not found.");

            entity = _staffMapper.MapToEntity(staff);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task RemoveAsync(int id)
        {
            StaffEntity entity = await _context.Set<StaffEntity>().FindAsync(id) ?? throw new EntityNotFoundException($"Staff with id {id} not found.");

            _context.Set<StaffEntity>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<StaffEntity>().FindAsync(id) != null ? true : false;
        }

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            List<StaffEntity> entities = await _context.Set<StaffEntity>()
                                                       .Include(e => e.TimeLogs)
                                                       .ToListAsync() ?? throw new EntityNotFoundException("No staff found.");
            return entities.Select(e => _staffMapper.MapToDomain(e));
        }

        public async Task AddRangeAsync(IEnumerable<Staff> staffs)
        {
            foreach (var staff in staffs)
            {
                if (staff == null)
                {
                    throw new ArgumentNullException(nameof(staff));
                }
            }

            List<StaffEntity> entities = staffs.Select(_staffMapper.MapToEntity).ToList();
            await _context.Staffs.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<int> staffIds)
        {
            List<StaffEntity> entities = await _context.Staffs.Where(u => staffIds.Contains(u.Id)).ToListAsync() ?? throw new EntityNotFoundException("No staff found.");
            _context.Staffs.RemoveRange(entities);
        }
    }
}
