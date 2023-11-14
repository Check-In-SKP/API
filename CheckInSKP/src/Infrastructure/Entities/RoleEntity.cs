using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CheckInSKP.Infrastructure.Entities
{
    public class RoleEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(64)]
        public string? Name { get; set; }
    }
}
