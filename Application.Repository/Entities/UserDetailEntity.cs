using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Application.Entities 
{
    public class UserDetailEntity
    {
        [Key]
        public long UserDetailId { get; set; }
        public long UserId { get; set; }
        public Guid AvatarFile { get; set; }
        public long EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }        

        [ForeignKey(nameof(UserId))]
        public UserEntity? User { get; set; }
    }
}
