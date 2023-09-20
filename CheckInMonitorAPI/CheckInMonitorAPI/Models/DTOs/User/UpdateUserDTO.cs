namespace CheckInMonitorAPI.Models.DTOs.User
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNotification { get; set; }
        public bool Preoccupied { get; set; }
        public DateTime? MeetingTime { get; set; }
        public int RoleId { get; set; }
    }
}
