namespace CheckInMonitorAPI.Models.DTOs.User
{
    public class CreateUserDTO
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNotification { get; set; }
        public bool Preoccupied { get; set; }
        public TimeOnly MeetingTime { get; set; }
        public int RoleId { get; set; }
    }
}
