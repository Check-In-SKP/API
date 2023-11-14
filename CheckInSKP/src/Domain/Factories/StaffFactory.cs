using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Entities.StaffAggregate;

namespace CheckInSKP.Domain.Factories
{
    public class StaffFactory
    {
        public Staff CreateStaff(Guid userId, string phoneNumber, string cardNumber, bool phoneNotification, bool preoccupied, TimeOnly meetingTime)
        {
            return new Staff(userId, phoneNumber, cardNumber, phoneNotification, preoccupied, meetingTime);
        }

        public Staff CreateNewStaff(Guid userId, string phoneNumber, string cardNumber, bool phoneNotification)
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
