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

        public TimeType? MapToDomain(TimeTypeEntity? entity)
        {
            if (entity == null)
                return null;

            return _timeTypeFactory.CreateTimeType(entity.Id, entity.Name);
        }

        public TimeTypeEntity MapToEntity(TimeType domain)
        {
            // Throws an null exceptions under the extreme circumstance that the domain is null.
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            return new TimeTypeEntity
            {
                Id = domain.Id,
                Name = domain.Name
            };
        }
    }
}
