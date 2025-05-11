using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Application.Entities
{
    public class MessageEntity
    {
        [Key]
        public long MessageId { get; set; }
        public long UserGroupId { get; set; }
        public long ReceiverUserGroupId { get; set; }
        public int MediaTypeId { get; set; }
        public string? MessageContent { get; set; }
        public Guid FileName { get; set; }
        public string? FileExt { get; set; }
        public string? MimeTypes { get; set; }
        public bool IsReply { get; set; }
        public bool IsSeen { get; set; }
        public long EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }


        [ForeignKey(nameof(UserGroupId))]
        public UserGroupEntity? UserGroup { get; set; } = null!;


        [ForeignKey(nameof(ReceiverUserGroupId))]
        public UserGroupEntity? ReceiverUserGroup { get; set; } = null!;


        [ForeignKey(nameof(MediaTypeId))]
        public MediaTypeEntity? MediaType { get; set; } = null!;
    }
}
