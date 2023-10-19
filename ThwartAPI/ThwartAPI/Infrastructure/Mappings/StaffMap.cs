using ThwartAPI.Domain.Aggregates.StaffAggregate;
using ThwartAPI.Infrastructure.Data.Entities;
using ThwartAPI.Infrastructure.Mappings.Interfaces;

namespace ThwartAPI.Infrastructure.Mappings
{
    public class StaffMap : IGenericMapper<Staff, StaffEntity>
    {
        public Staff MapToDomain(StaffEntity entity)
        {
            throw new NotImplementedException();
        }

        public StaffEntity MapToEntity(Staff domain)
        {
            throw new NotImplementedException();
        }
    }
}
