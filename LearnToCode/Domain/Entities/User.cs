using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeveloperHub.Domain.Entities
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
        [MaxLength(100)]
        public required string PermissionLevel { get; set; }

        [Column("Name")]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Column("ProfilePicture")]
        public string? ImageBytes { get; set; }
    }
}
