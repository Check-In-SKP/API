using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ThwartAPI.Infrastructure.Data.Entities
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string CardNumber { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(128)]
        public string Username { get; set; }

        [Required]
        [MaxLength(128)]
        public string Password { get; set; }

        [MaxLength(64)]
        public string PhoneNumber { get; set; }

        [Required]
        public bool PhoneNotification { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Preoccupied { get; set; }

        [Required]
        public DateTime? MeetingTime { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public RoleEntity Role { get; set; }
    }
}
