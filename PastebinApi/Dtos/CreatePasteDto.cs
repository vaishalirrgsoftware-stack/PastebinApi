namespace PastebinApi.Dtos;

public class CreatePasteDto
{
    public string Content { get; set; }
    public int? ExpiresInMinutes { get; set; }
    public int? MaxViews { get; set; }
}
