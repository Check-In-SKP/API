namespace ThwartAPI.Domain.Entities
{
    public class TimeLog
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public TimeType TimeType { get; set; }
        public User User { get; set; }

        public TimeLog(DateTime timeStamp, TimeType timeType, User user)
        {
            TimeStamp = timeStamp;
            TimeType = timeType ?? throw new ArgumentNullException(nameof(timeType));
            User = user ?? throw new ArgumentNullException(nameof(user));
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
