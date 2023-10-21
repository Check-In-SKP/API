using Domain.Entities.StaffAggregate;
using Domain.Factories;
using Infrastructure.Data.Entities;
using Infrastructure.Mappings.Interfaces;

namespace Infrastructure.Mappings
{
    public class StaffMapper : IGenericMapper<Staff, StaffEntity>
    {
        private readonly StaffFactory _staffFactory;

        public StaffMapper(StaffFactory staffFactory)
        {
            _staffFactory = staffFactory;
        }

        public Staff MapToDomain(StaffEntity entity)
        {
            // Maps entity timelogs from staff to domain timelogs
            var timelogs = entity.TimeLogs.Select(MapTimeLogToDomain).ToList();

            return _staffFactory.CreateStaff(entity.Id, entity.UserId, entity.PhoneNumber, entity.CardNumber, entity.PhoneNotification, entity.Preoccupied, entity.MeetingTime);
        }

        public StaffEntity MapToEntity(Staff domain)
        {
            // Maps domain timelogs from staff to entity timelogs
            var timelogs = domain.TimeLogs.Select(MapTimeLogToEntity).ToList();

            return new StaffEntity
            {
                Id = domain.Id,
                UserId = domain.UserId,
                PhoneNumber = domain.PhoneNumber,
                CardNumber = domain.CardNumber,
                PhoneNotification = domain.PhoneNotification,
                Preoccupied = domain.Preoccupied,
                MeetingTime = domain.MeetingTime,
                TimeLogs = timelogs
            };
        }

        private TimeLog MapTimeLogToDomain(TimeLogEntity entity)
        {
            return _staffFactory.CreateTimeLog(entity.Id, entity.TimeStamp, entity.TimeTypeId);
        }

        private TimeLogEntity MapTimeLogToEntity(TimeLog domain)
        {
            return new TimeLogEntity
            {
                Id = domain.Id,
                TimeStamp = domain.TimeStamp,
                TimeTypeId = domain.TimeTypeId
            };
        }
    }
}
