using ThwartAPI.Domain.Entities;
using ThwartAPI.Domain.Entities.StaffAggregate;
using ThwartAPI.Domain.Entities.UserAggregate;

namespace ThwartAPI.Domain.Factories
{
    public class StaffFactory
    {
        public Staff CreateStaff(int id, User user, string phoneNumber, string cardNumber, bool phoneNotification, bool preoccupied, TimeOnly meetingTime)
        {
            return new Staff(id, user, phoneNumber, cardNumber, phoneNotification, preoccupied, meetingTime);
        }

        public Staff CreateNewStaff(User user, string phoneNumber, string cardNumber, bool phoneNotification)
        {
            return new Staff(user, phoneNumber, cardNumber, phoneNotification);
        }
        
        public TimeLog CreateTimeLog(int id, DateTime timeStamp, TimeType timeType)
        {
            return new TimeLog(id, timeStamp, timeType);
        }

        public TimeLog CreateNewTimeLog(DateTime timeStamp, TimeType timeType)
        {
            return new TimeLog(timeStamp, timeType);
        }
    }
}
