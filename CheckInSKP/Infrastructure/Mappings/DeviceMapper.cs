using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Factories;
using CheckInSKP.Infrastructure.Mappings.Interfaces;
using CheckInSKP.Infrastructure.Entities;

namespace CheckInSKP.Infrastructure.Mappings
{
    public class DeviceMapper : IGenericMapper<Device, DeviceEntity>
    {
        private readonly DeviceFactory _deviceFactory;
        public DeviceMapper(DeviceFactory deviceFactory)
        {
            _deviceFactory = deviceFactory ?? throw new ArgumentNullException(nameof(deviceFactory));
        }

        public Device MapToDomain(DeviceEntity entity)
        {
            return _deviceFactory.CreateDevice(entity.Id, entity.Label, entity.Authorized);
        }

        public DeviceEntity MapToEntity(Device domain)
        {
            return new DeviceEntity
            {
                Id = domain.Id,
                Label = domain.Label,
                Authorized = domain.Authorized
            };
        }
    }
}
