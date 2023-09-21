using System.ComponentModel.DataAnnotations;

namespace CheckInMonitorAPI.Models.DTOs.TimeLog
{
    public class CreateTimeLogDTO
    {
        public DateTime TimeStamp { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TimeTypeId { get; set; }
    }
}
