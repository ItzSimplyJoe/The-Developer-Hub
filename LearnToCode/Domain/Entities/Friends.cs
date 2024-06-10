using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DeveloperHub.Domain.Entities
{
    public record Friends
    {
        [Key]
        [Column("Id")]
        public Guid FriendShipId { get; set; }

        [ForeignKey("User")]
        [Column("UserId")]
        public Guid UserId { get; set; }

        [Column("FriendId")]
        public Guid FriendId { get; set; }

        [Column("FriendShipStarted")]
        public DateTime FriendshipStarted { get; set; }

        public virtual required User User { get; set; }
    }
}
