using Microsoft.AspNetCore.Http.HttpResults;

namespace ThwartAPI.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string PhoneNumber { get; private set; }
        public string CardNumber { get; private set; }
        public bool PhoneNotification { get; private set; }
        public bool Preoccupied { get; private set; }
        public TimeOnly MeetingTime { get; private set; }
        public Role Role { get; private set; }

        public User(string name, string username, string passwordHash, string phoneNumber, string cardNumber, bool phoneNotification, Role role)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Username = username ?? throw new ArgumentNullException(nameof(username));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            PhoneNumber = phoneNumber;
            CardNumber = cardNumber ?? throw new ArgumentNullException(nameof(cardNumber));
            PhoneNotification = phoneNotification;
            Role = role ?? throw new ArgumentNullException(nameof(role));
            Preoccupied = false;
            MeetingTime = new TimeOnly(8, 10, 0);
        }

        public void SetMeetingTime(TimeOnly newMeetingTime)
        {
            MeetingTime = newMeetingTime;
        }

        public void SetPreoccupied(bool newPreoccupied)
        {
            Preoccupied = newPreoccupied;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrEmpty(newName) || newName.Length > 64)
            {
                throw new ArgumentException("Invalid new user name.");
            }

            Name = newName;
        }

        public void UpdateUsername(string newUsername)
        {
            if (string.IsNullOrEmpty(newUsername) || newUsername.Length > 128)
            {
                throw new ArgumentException("Invalid new username.");
            }

            Username = newUsername;
        }

        public void UpdatePasswordHash(string newPasswordHash)
        {
            if (string.IsNullOrEmpty(newPasswordHash) || newPasswordHash.Length > 128)
            {
                throw new ArgumentException("Invalid new password hash.");
            }

            PasswordHash = newPasswordHash;
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

        public void UpdateRole(Role newRole)
        {

            Role = newRole ?? throw new ArgumentNullException(nameof(newRole));
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
