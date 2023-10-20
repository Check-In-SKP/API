using ThwartAPI.Domain.Entities;
using ThwartAPI.Domain.Factories;
using ThwartAPI.Infrastructure.Data.Entities;
using ThwartAPI.Infrastructure.Mappings.Interfaces;

namespace ThwartAPI.Infrastructure.Mappings
{
    public class TimeTypeMap : IGenericMapper<TimeType, TimeTypeEntity>
    {
 
        private readonly TimeTypeFactory _timeTypeFactory;

        public TimeTypeMap(TimeTypeFactory timeTypeFactory)
        {
            _timeTypeFactory = timeTypeFactory;
        }

        public TimeType MapToDomain(TimeTypeEntity entity)
        {
            return _timeTypeFactory.CreateTimeType(entity.Id, entity.Name);
        }

        public TimeTypeEntity MapToEntity(TimeType domain)
        {
            return new TimeTypeEntity
            {
                Id = domain.Id,
                Name = domain.Name
            };
        }
    }
}
