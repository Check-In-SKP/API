﻿using ThwartAPI.Domain.Entities;
using ThwartAPI.Domain.Factories;
using ThwartAPI.Infrastructure.Data.Entities;
using ThwartAPI.Infrastructure.Mappings.Interfaces;

namespace ThwartAPI.Infrastructure.Mappings
{
    public class DeviceMap : IGenericMapper<Device, DeviceEntity>
    {
        private readonly DeviceFactory _deviceFactory;
        public DeviceMap(DeviceFactory deviceFactory)
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