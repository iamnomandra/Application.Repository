using System.ComponentModel.DataAnnotations;

namespace Chat.Application.Entities
{
    public class  GroupEntity
    {
        [Key]
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public long EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }
        public ICollection<UserGroupEntity>? UserGroup { get; set; } = new List<UserGroupEntity>();
    }
}
