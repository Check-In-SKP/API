using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.StaffAggregate
{
    public class Staff
    {
        private readonly int _id;
        public int Id => _id;

        [Required, StringLength(64)]
        public string PhoneNumber { get; private set; }

        [Required, StringLength(128)]
        public string CardNumber { get; private set; }

        public bool PhoneNotification { get; private set; }
        public bool Preoccupied { get; private set; }

        public TimeOnly MeetingTime { get; private set; }

        private ImmutableList<TimeLog> _timeLogs = ImmutableList<TimeLog>.Empty;
        public IReadOnlyList<TimeLog> TimeLogs => _timeLogs;

        public int UserId { get; private set; }

        // Constructor for new Staff
        public Staff(int userId, string phoneNumber, string cardNumber, bool phoneNotification)
        {
            ValidateInput(userId, phoneNumber, cardNumber);

            UserId = userId;
            PhoneNumber = phoneNumber;
            CardNumber = cardNumber;
            PhoneNotification = phoneNotification;
            Preoccupied = false;
            MeetingTime = new TimeOnly(8, 10, 0);
        }

        // Constructor for existing Staff
        public Staff(int id, int userId, string phoneNumber, string cardNumber, bool phoneNotification, bool preoccupied, TimeOnly meetingTime)
        {
            ValidateInput(userId, phoneNumber, cardNumber);

            _id = id;
            UserId = userId;
            PhoneNumber = phoneNumber;
            CardNumber = cardNumber;
            PhoneNotification = phoneNotification;
            Preoccupied = preoccupied;
            MeetingTime = meetingTime;
        }

        private void ValidateInput(int userId, string phoneNumber, string cardNumber)
        {
            if (userId <= 0) throw new ArgumentException("Invalid user ID.", nameof(userId));
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length > 64)
                throw new ArgumentException("Invalid phone number.", nameof(phoneNumber));
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length > 128)
                throw new ArgumentException("Invalid card number.", nameof(cardNumber));
        }

        public void AddTimeLog(TimeLog timeLog)
        {
            if (timeLog == null) throw new ArgumentNullException(nameof(timeLog));
            _timeLogs = _timeLogs.Add(timeLog);
        }

        public void RemoveTimeLog(TimeLog timeLog)
        {
            if (timeLog == null) throw new ArgumentNullException(nameof(timeLog));
            _timeLogs = _timeLogs.Remove(timeLog);
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
