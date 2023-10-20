using ThwartAPI.Domain.Entities;
using ThwartAPI.Domain.Entities.StaffAggregate;
using ThwartAPI.Domain.Entities.UserAggregate;

namespace ThwartAPI.Domain.Factories
{
    public class StaffFactory
    {
        public Staff CreateStaff(int id, int userId, string phoneNumber, string cardNumber, bool phoneNotification, bool preoccupied, TimeOnly meetingTime)
        {
            return new Staff(id, userId, phoneNumber, cardNumber, phoneNotification, preoccupied, meetingTime);
        }

        public Staff CreateNewStaff(int userId, string phoneNumber, string cardNumber, bool phoneNotification)
        {
            return new Staff(userId, phoneNumber, cardNumber, phoneNotification);
        }
        
        public TimeLog CreateTimeLog(int id, DateTime timeStamp, int timeTypeId)
        {
            return new TimeLog(id, timeStamp, timeTypeId);
        }

        public TimeLog CreateNewTimeLog(DateTime timeStamp, int timeTypeId)
        {
            return new TimeLog(timeStamp, timeTypeId);
        }
    }
}
