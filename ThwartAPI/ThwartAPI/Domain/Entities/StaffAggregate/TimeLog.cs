using System.ComponentModel.DataAnnotations;
using ThwartAPI.Domain.Entities.UserAggregate;
using ThwartAPI.Domain.Entities;

namespace ThwartAPI.Domain.Entities.StaffAggregate
{
    public class TimeLog
    {
        private readonly int _id;
        public int Id => _id;

        [Required]
        public DateTime TimeStamp { get; private set; }

        [Required]
        public TimeType TimeType { get; private set; }

        // Constructor for new TimeLog
        public TimeLog(DateTime timeStamp, TimeType timeType)
        {
            ValidateInput(timeStamp, timeType);

            TimeStamp = timeStamp;
            TimeType = timeType;
        }

        // Constructor for existing TimeLog
        public TimeLog(int id, DateTime timeStamp, TimeType timeType)
        {
            ValidateInput(timeStamp, timeType);

            _id = id;
            TimeStamp = timeStamp;
            TimeType = timeType;
        }

        private void ValidateInput(DateTime timeStamp, TimeType timeType)
        {
            if (timeStamp == DateTime.MinValue)
                throw new ArgumentException("Invalid time stamp.", nameof(timeStamp));

            if (timeType == null)
                throw new ArgumentNullException(nameof(timeType));
        }

        public void UpdateTimeStamp(DateTime newTimeStamp)
        {
            if (newTimeStamp == DateTime.MinValue)
                throw new ArgumentException("Invalid new time stamp.", nameof(newTimeStamp));

            TimeStamp = newTimeStamp;
        }

        public void UpdateTimeType(TimeType newTimeType)
        {
            if (newTimeType == null)
                throw new ArgumentNullException(nameof(newTimeType));

            TimeType = newTimeType;
        }
    }
}
