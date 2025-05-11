
namespace Chat.Application.Entities 
{
    public class ApiTokenEntity
    {
        public int ApiId { get; set; }

        public string? ApiKey { get; set; }

        public int? ModuleId { get; set; } 

        public int? Permission { get; set; }

        public string? RefreshToken { get; set; } 

        public DateTime? ExpiryDate { get; set; }

        public bool? Enabled { get; set; }

        public DateTime? CreatedTime { get; set; }

        public DateTime? LastUpdTime { get; set; }
    }
}
