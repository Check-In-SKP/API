namespace ThwartAPI.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Role(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length > 64)
            {
                throw new ArgumentException("Invalid role name.");
            }

            Name = name;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrEmpty(newName) || newName.Length > 64)
            {
                throw new ArgumentException("Invalid new role name.");
            }

            Name = newName;
        }
    }
}
