using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Factories;
using CheckInSKP.Infrastructure.Entities;
using CheckInSKP.Infrastructure.Mappings.Interfaces;

namespace CheckInSKP.Infrastructure.Mappings
{
    public class TimeTypeMapper : IGenericMapper<TimeType, TimeTypeEntity>
    {

        private readonly TimeTypeFactory _timeTypeFactory;

        public TimeTypeMapper(TimeTypeFactory timeTypeFactory)
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
