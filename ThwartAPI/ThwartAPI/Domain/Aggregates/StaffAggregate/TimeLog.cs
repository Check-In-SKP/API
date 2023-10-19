using ThwartAPI.Domain.Aggregates.UserAggregate;
using ThwartAPI.Domain.Entities;

namespace ThwartAPI.Domain.Aggregates.StaffAggregate
{
    public class TimeLog
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public TimeType TimeType { get; set; }

        public TimeLog(DateTime timeStamp, TimeType timeType)
        {
            TimeStamp = timeStamp;
            TimeType = timeType ?? throw new ArgumentNullException(nameof(timeType));
        }

        public void UpdateTimeStamp(DateTime newTimeStamp)
        {
            TimeStamp = newTimeStamp;
        }

        public void UpdateTimeType(TimeType newTimeType)
        {
            TimeType = newTimeType ?? throw new ArgumentNullException(nameof(newTimeType));
        }
    }
}
