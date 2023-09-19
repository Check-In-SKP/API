using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CheckInMonitorAPI.Models.Entities
{
    public class TimeLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public int TimeTypeId { get; set; }

        [ForeignKey(nameof(TimeTypeId))]
        public TimeType TimeType { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
