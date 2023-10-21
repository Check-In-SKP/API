using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Role
    {
        private readonly int _id;
        public int Id => _id;

        [Required, StringLength(64)]
        public string Name { get; private set; }

        // Constructor for new Role
        public Role(string name)
        {
            ValidateInput(name);

            Name = name;
        }

        // Constructor for existing Role
        public Role(int id, string name)
        {
            ValidateInput(name);

            _id = id;
            Name = name;
        }

        private void ValidateInput(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 64)
                throw new ArgumentException("Invalid role name.", nameof(name));
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName) || newName.Length > 64)
                throw new ArgumentException("Invalid new role name.", nameof(newName));

            Name = newName;
        }
    }
}
