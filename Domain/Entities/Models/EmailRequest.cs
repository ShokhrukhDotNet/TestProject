namespace Domain.Entities.Models;

public class EmailRequest
{
    public List<int> UserIds { get; set; } = new();
    public string? Title { get; set; }
    public string? Description { get; set; }
}
