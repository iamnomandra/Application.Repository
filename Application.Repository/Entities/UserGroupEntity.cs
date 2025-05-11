using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Application.Entities
{
    public class UserGroupEntity
    {
        [Key] 
        public int UserGroupId { get; set; }
        public int GroupId { get; set; }
        public long UserId { get; set; }
        public bool DefaultRoom { get; set; }
        public string? LastMessage { get; set; }
        public DateTime LastMagDate { get; set; }
        public int TotalUnSeen { get; set; }
        public long EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }

        [ForeignKey("GroupId")]
        public GroupEntity? Group { get; set; } = null!;

        [ForeignKey("UserId")]
        public UserEntity? User { get; set; } = null!;



        [InverseProperty("UserGroup")]
        public List<MessageEntity>? Messages { get; set; } = new List<MessageEntity>();

        [InverseProperty("ReceiverUserGroup")]
        public List<MessageEntity>? ReceiverMessages { get; set; } = new List<MessageEntity>();
    }
}
