using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CheckInSKP.Infrastructure.Entities
{
    public class TimeTypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(64)]
        public required string Name { get; set; }
    }
}
