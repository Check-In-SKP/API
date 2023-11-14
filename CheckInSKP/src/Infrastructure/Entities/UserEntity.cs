using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CheckInSKP.Infrastructure.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(64)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(128)]
        public required string Username { get; set; }

        [Required]
        [MaxLength(256)]
        public required string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public RoleEntity? Role { get; set; }

        public StaffEntity? Staff { get; set; }
    }
}
