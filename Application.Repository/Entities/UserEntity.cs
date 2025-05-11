using System.ComponentModel.DataAnnotations; 

namespace Chat.Application.Entities
{
    public class UserEntity
    {
        [Key]
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? ContactNumber { get; set; }
        public long EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }= DateTime.Now;        
        public UserDetailEntity? UserDetail { get; set; } = null!;
        public ICollection<UserGroupEntity>? UserGroups { get; set; } =  new List<UserGroupEntity>();
    }
}
