using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThwartAPI.Infrastructure.Data.Entities
{
    public class StaffEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string CardNumber { get; set; }

        [MaxLength(64)]
        public string PhoneNumber { get; set; }

        [Required]
        public bool PhoneNotification { get; set; }
        public bool Preoccupied { get; set; }

        [Required]
        public TimeOnly MeetingTime { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; }
    }
}
