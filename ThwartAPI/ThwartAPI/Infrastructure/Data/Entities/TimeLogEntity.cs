using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThwartAPI.Infrastructure.Data.Entities
{
    public class TimeLogEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public int TimeTypeId { get; set; }

        [ForeignKey(nameof(TimeTypeId))]
        public TimeTypeEntity TimeType { get; set; }

        public int StaffId { get; set; }

        [ForeignKey(nameof(StaffId))]
        public StaffEntity Staff { get; set; }
    }
}
