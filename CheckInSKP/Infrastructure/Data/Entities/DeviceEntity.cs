using System.ComponentModel.DataAnnotations;

namespace CheckInSKP.Infrastructure.Data.Entities
{
    public class DeviceEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Label { get; set; }

        [Required]
        public bool Authorized { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
