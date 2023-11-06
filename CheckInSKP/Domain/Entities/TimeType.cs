using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CheckInSKP.Domain.Common;
using CheckInSKP.Domain.Enums;

namespace CheckInSKP.Domain.Entities
{

    public class TimeType : DomainEntity
    {
        private readonly int _id;
        public int Id => _id;

        [Required, StringLength(64)]
        public string Name { get; private set; }

        // Constructor for new TimeType
        public TimeType(string name)
        {
            ValidateInput(name);

            Name = name;
        }

        // Constructor for existing TimeType
        public TimeType(int id, string name)
        {
            ValidateInput(name);

            _id = id;
            Name = name;
        }

        private void ValidateInput(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 64)
                throw new ArgumentException("Invalid name.", nameof(name));
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName) || newName.Length > 64)
                throw new ArgumentException("Invalid new time type name.", nameof(newName));
            Name = newName;
        }

        public static string GetNameFromEnum(TimeTypeEnum timeType)
        {
            return timeType.ToString();
        }
    }
}
