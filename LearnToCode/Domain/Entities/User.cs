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

        [Column("FirstName")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Column("LastName")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Column("Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column("Country")]
        [MaxLength(100)]
        public string? Country { get; set; }

        [Column("City")]
        [MaxLength(100)]
        public string? City { get; set; }

        [Column("Address")]
        [MaxLength(100)]
        public string? Address { get; set; }

        [Phone]
        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        [Column("Birthday")]
        public DateTime? Birthday { get; set; }

        [Column("DateCreated")]
        public DateTime DateCreated { get; set; }

        [Column("DateUpdated")]
        public DateTime? DateUpdated { get; set; }

        [Column("Organisation")]
        [MaxLength(100)]
        public string? Organisation { get; set; }

        [Column("Role")]
        [MaxLength(100)]
        public string? Role { get; set; }

        [Column("Department")]
        [MaxLength(100)]
        public string? Department { get; set; }

        [Column("PostalCode")]
        [MaxLength(100)]
        public string? PostalCode { get; set; }

        [Column("ProfilePicture")]
        public string? ImageBytes { get; set; }

        public virtual IEnumerable<Friends>? Friends { get; set; }
    }
}
