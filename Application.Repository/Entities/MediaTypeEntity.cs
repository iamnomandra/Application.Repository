using System.ComponentModel.DataAnnotations;

namespace Chat.Application.Entities
{
    public class MediaTypeEntity 
    {
        [Key]  
        public int MediaTypeId { get; set; }
        public string? MediaTypeName { get; set; }
        public long EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }
        public ICollection<MessageEntity>? Messages { get; set; } = new List<MessageEntity>();
    }
}
