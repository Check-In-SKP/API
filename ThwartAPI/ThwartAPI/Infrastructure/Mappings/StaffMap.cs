using Microsoft.AspNetCore.Razor.TagHelpers;
using ThwartAPI.Domain.Entities.StaffAggregate;
using ThwartAPI.Domain.Factories;
using ThwartAPI.Infrastructure.Data.Entities;
using ThwartAPI.Infrastructure.Mappings.Interfaces;

namespace ThwartAPI.Infrastructure.Mappings
{
    public class StaffMap : IGenericMapper<Staff, StaffEntity>
    {
        private readonly UserMap _userMap;
        private readonly TimeTypeMap _timeTypeMap;
        private readonly TimeTypeFactory _timeTypeFactory;
        private readonly StaffFactory _staffFactory;

        public StaffMap(UserMap userMap, TimeTypeMap timeTypeMap, TimeTypeFactory timeTypeFactory, StaffFactory staffFactory)
        {
            _userMap = userMap;
            _timeTypeMap = timeTypeMap;
            _timeTypeFactory = timeTypeFactory;
            _staffFactory = staffFactory;
        }

        public Staff MapToDomain(StaffEntity entity)
        {
            // Maps entity timelogs from staff to domain timelogs
            var timelogs = entity.TimeLogs.Select(MapTimeLogToDomain).ToList();

            return _staffFactory.CreateStaff(entity.Id, _userMap.MapToDomain(entity.User), entity.PhoneNumber, entity.CardNumber, entity.PhoneNotification, entity.Preoccupied, entity.MeetingTime);
        }

        public StaffEntity MapToEntity(Staff domain)
        {
            // Maps domain timelogs from staff to entity timelogs
            var timelogs = domain.TimeLogs.Select(MapTimeLogToEntity).ToList();

            return new StaffEntity
            {
                Id = domain.Id,
                User = _userMap.MapToEntity(domain.User),
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
            // Maps entity timetype to domain timetype
            var timetype = _timeTypeMap.MapToDomain(entity.TimeType);

            return _staffFactory.CreateTimeLog(entity.Id, entity.TimeStamp, timetype);
        }

        private TimeLogEntity MapTimeLogToEntity(TimeLog domain)
        {
            // Maps domain timetype to entity timetype
            var timetype = _timeTypeMap.MapToEntity(domain.TimeType);

            return new TimeLogEntity
            {
                Id = domain.Id,
                TimeStamp = domain.TimeStamp,
                TimeType = timetype
            };
        }
    }
}
