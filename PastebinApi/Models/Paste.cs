namespace PastebinApi.Models
{
    public class Paste
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..8];
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
        public int? MaxViews { get; set; }
        public int CurrentViews { get; set; } = 0;
    }
}
