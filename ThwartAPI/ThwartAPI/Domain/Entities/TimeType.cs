using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThwartAPI.Domain.Entities
{
    public class TimeType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TimeType(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 64)
            {
                throw new ArgumentException("Invalid time type name.");
            }

            Name = name;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrEmpty(newName) || newName.Length > 64)
            {
                throw new ArgumentException("Invalid new time type name.");
            }

            Name = newName;
        }
    }
}
