using ThwartAPI.Domain.Aggregates.UserAggregate;
using ThwartAPI.Domain.Entities;

namespace ThwartAPI.Domain.Aggregates.StaffAggregate
{
    public class Staff
    {
        public int Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string CardNumber { get; private set; }
        public bool PhoneNotification { get; private set; }
        public bool Preoccupied { get; private set; }
        public TimeOnly MeetingTime { get; private set; }
        public ICollection<TimeLog> TimeLogs { get; private set; } = new List<TimeLog>();
        public User User { get; private set; }

        public Staff(User user, string phoneNumber, string cardNumber, bool phoneNotification)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            PhoneNumber = phoneNumber;
            CardNumber = cardNumber ?? throw new ArgumentNullException(nameof(cardNumber));
            PhoneNotification = phoneNotification;
            Preoccupied = false;
            MeetingTime = new TimeOnly(8, 10, 0);
        }

        public void AddTimeLog(TimeLog timeLog)
        {
            TimeLogs.Add(timeLog ?? throw new ArgumentNullException(nameof(timeLog)));
        }

        public void RemoveTimeLog(TimeLog timeLog)
        {
            TimeLogs.Remove(timeLog ?? throw new ArgumentNullException(nameof(timeLog)));
        }

        public void SetMeetingTime(TimeOnly newMeetingTime)
        {
            MeetingTime = newMeetingTime;
        }

        public void SetPreoccupied(bool newPreoccupied)
        {
            Preoccupied = newPreoccupied;
        }

        public void UpdatePhoneNumber(string newPhoneNumber)
        {
            if (string.IsNullOrEmpty(newPhoneNumber) || newPhoneNumber.Length > 64)
            {
                throw new ArgumentException("Invalid new phone number.");
            }

            PhoneNumber = newPhoneNumber;
        }

        public void UpdatePhoneNotification(bool newPhoneNotification)
        {
            PhoneNotification = newPhoneNotification;
        }

        public void UpdatePreoccupied(bool newPreoccupied)
        {
            Preoccupied = newPreoccupied;
        }

        public void UpdateMeetingTime(TimeOnly newMeetingTime)
        {
            MeetingTime = newMeetingTime;
        }

        public void UpdateCardNumber(string newCardNumber)
        {
            if (string.IsNullOrEmpty(newCardNumber) || newCardNumber.Length > 128)
            {
                throw new ArgumentException("Invalid new card number.");
            }

            CardNumber = newCardNumber;
        }
    }
}
