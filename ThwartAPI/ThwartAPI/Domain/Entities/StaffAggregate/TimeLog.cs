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
        public int TimeTypeId { get; private set; }

        // Constructor for new TimeLog
        internal TimeLog(DateTime timeStamp, int timeTypeId)
        {
            ValidateInput(timeStamp, timeTypeId);

            TimeStamp = timeStamp;
            TimeTypeId = timeTypeId;
        }

        // Constructor for existing TimeLog
        internal TimeLog(int id, DateTime timeStamp, int timeTypeId)
        {
            ValidateInput(timeStamp, timeTypeId);

            _id = id;
            TimeStamp = timeStamp;
            TimeTypeId = timeTypeId;
        }

        private void ValidateInput(DateTime timeStamp, int timeTypeId)
        {
            if (timeStamp == DateTime.MinValue)
                throw new ArgumentException("Invalid time stamp.", nameof(timeStamp));

            if (timeTypeId <= 0)
                throw new ArgumentException("Invalid time type ID.", nameof(timeTypeId));
        }

        public void UpdateTimeStamp(DateTime newTimeStamp)
        {
            if (newTimeStamp == DateTime.MinValue)
                throw new ArgumentException("Invalid new time stamp.", nameof(newTimeStamp));

            TimeStamp = newTimeStamp;
        }

        public void UpdateTimeType(int newTimeTypeId)
        {
            if (newTimeTypeId <= 0)
                throw new ArgumentException("Invalid time type ID.", nameof(newTimeTypeId));

            TimeTypeId = newTimeTypeId;
        }
    }
}
