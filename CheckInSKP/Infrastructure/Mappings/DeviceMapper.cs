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

        public Device? MapToDomain(DeviceEntity? entity)
        {
            if (entity == null)
                return null;

            return _deviceFactory.CreateDevice(entity.Id, entity.Label, entity.IsAuthorized);
        }

        public DeviceEntity MapToEntity(Device domain)
        {
            // Throws an null exceptions under the extreme circumstance that the domain is null.
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            return new DeviceEntity
            {
                Id = domain.Id,
                Label = domain.Label,
                IsAuthorized = domain.IsAuthorized
            };
        }
    }
}
