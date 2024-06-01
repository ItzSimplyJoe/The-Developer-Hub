using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DeveloperHub.Domain.Entities
{
    public class Friends
    {
        [Key]
        [Column("Id")]
        public Guid FriendShipID { get; set; }

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
