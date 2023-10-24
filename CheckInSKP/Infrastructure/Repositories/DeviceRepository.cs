using Microsoft.EntityFrameworkCore;
using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Interfaces.Repositories;
using CheckInSKP.Infrastructure.Mappings;
using CheckInSKP.Infrastructure.Data;
using CheckInSKP.Infrastructure.Exceptions;
using CheckInSKP.Infrastructure.Entities;

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

        public async Task AddAsync(Device device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            DeviceEntity entity = _deviceMapper.MapToEntity(device);
            await _context.Devices.AddAsync(entity);
        }

        public async Task<Device> GetByIdAsync(Guid id)
        {
            DeviceEntity entity = await _context.Set<DeviceEntity>()
                                               .FirstOrDefaultAsync(e => e.Id == id) ?? throw new EntityNotFoundException($"Device with id {id} not found.");

            return _deviceMapper.MapToDomain(entity);
        }


        public async Task UpdateAsync(Device device)
        {
            DeviceEntity entity = await _context.Set<DeviceEntity>().FindAsync(device.Id) ?? throw new EntityNotFoundException($"Device with id {device.Id} not found.");

            entity = _deviceMapper.MapToEntity(device);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task RemoveAsync(Guid id)
        {
            DeviceEntity entity = await _context.Set<DeviceEntity>().FindAsync(id) ?? throw new EntityNotFoundException($"Device with id {id} not found.");

            _context.Set<DeviceEntity>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Set<DeviceEntity>().FindAsync(id) != null ? true : false;
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            IEnumerable<DeviceEntity> entities = await _context.Set<DeviceEntity>().ToListAsync() ?? throw new EntityNotFoundException("No devices found.");
            return entities.Select(e => _deviceMapper.MapToDomain(e));
        }

        public async Task AddRangeAsync(IEnumerable<Device> devices)
        {
            foreach (var device in devices)
            {
                if (device == null)
                {
                    throw new ArgumentNullException(nameof(device));
                }
            }

            List<DeviceEntity> entities = devices.Select(_deviceMapper.MapToEntity).ToList();
            await _context.Devices.AddRangeAsync(entities);
        }

        public async Task RemoveRangeAsync(IEnumerable<Device> devices)
        {
            IEnumerable<Guid> deviceIds = devices.Select(u => u.Id);
            List<DeviceEntity> entities = await _context.Devices.Where(u => deviceIds.Contains(u.Id)).ToListAsync() ?? throw new EntityNotFoundException("No devices found.");
            _context.Devices.RemoveRange(entities);
        }
    }
}
