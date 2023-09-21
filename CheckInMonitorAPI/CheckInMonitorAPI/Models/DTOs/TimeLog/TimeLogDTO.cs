using System.ComponentModel.DataAnnotations;

namespace CheckInMonitorAPI.Models.DTOs.TimeLog
{
    public class TimeLogDTO
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TimeTypeId { get; set; }
    }
}
