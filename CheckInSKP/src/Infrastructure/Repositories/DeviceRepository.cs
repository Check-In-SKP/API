using Microsoft.EntityFrameworkCore;
using CheckInSKP.Domain.Entities;
using CheckInSKP.Infrastructure.Mappings;
using CheckInSKP.Infrastructure.Data;
using CheckInSKP.Infrastructure.Entities;
using CheckInSKP.Domain.Repositories;

namespace CheckInSKP.Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DeviceMapper _deviceMapper;

        public DeviceRepository(ApplicationDbContext context, DeviceMapper deviceMapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _deviceMapper = deviceMapper ?? throw new ArgumentNullException(nameof(deviceMapper));
        }

        public async Task<Device?> AddAsync(Device device)
        {
            var entity = _deviceMapper.MapToEntity(device);
            var addedEntity = await _context.Devices.AddAsync(entity);
            return _deviceMapper.MapToDomain(addedEntity.Entity);
        }

        public async Task<Device?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Devices.FindAsync(id);
            return _deviceMapper.MapToDomain(entity);
        }

        public async Task UpdateAsync(Device device)
        {
            var entity = await _context.Set<DeviceEntity>().FindAsync(device.Id);
            if(entity != null)
            {
                entity.Label = device.Label;
                entity.IsAuthorized = device.IsAuthorized;
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await _context.Set<DeviceEntity>().FindAsync(id);

            if(entity != null)
            {
                _context.Set<DeviceEntity>().Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Set<DeviceEntity>().FindAsync(id) != null;
        }

        public async Task<IEnumerable<Device?>> GetAllAsync()
        {
            var entities = await _context.Devices.ToListAsync();
            return entities.Select(_deviceMapper.MapToDomain);
        }
        public async Task<IEnumerable<Device?>> GetWithPaginationAsync(int page, int pageSize)
        {
            var entities = await _context.Set<DeviceEntity>().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return entities.Select(_deviceMapper.MapToDomain);
        }

        public async Task AddRangeAsync(IEnumerable<Device> devices)
        {
            var entities = devices.Select(_deviceMapper.MapToEntity).ToList();
            await _context.Devices.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<Guid> deviceIds)
        {
            var entities = await _context.Devices.Where(u => deviceIds.Contains(u.Id)).ToListAsync();
            if (entities != null)
            {
                _context.Devices.RemoveRange(entities);
            }
        }

        public IQueryable<Device?> Query() => _context.Set<DeviceEntity>().Select(e => _deviceMapper.MapToDomain(e)).AsQueryable();

    }
}
