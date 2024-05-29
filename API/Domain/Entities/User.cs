using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entities
{
    [Table("Users")]
    public record User
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Email")]
        [MaxLength(100)]
        public required string Email { get; set; }

        [Column("Password")]
        [MaxLength(100)]
        public required string Password { get; set; }

        [Column("PermissionLevel")]
        public PermissionLevel PermissionLevel { get; set; }
    }
}
