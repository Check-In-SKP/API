namespace CheckInMonitorAPI.Models.DTOs.TimeLog
{
    public class CreateTimeLogDTO
    {
        public int UserId { get; set; }
        public int TimeTypeId { get; set; }
        public int RoleId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
