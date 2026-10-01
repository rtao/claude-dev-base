namespace PokerTracker.Domain.Models;

public class HandHistory
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public Session Session { get; set; } = null!;
    public string? FreeText { get; set; }
    public string? Position { get; set; }
    public string? HoleCards { get; set; }
    public string? BoardCards { get; set; }
    public string? ActionSequence { get; set; }  // JSON string
    public decimal? PotSize { get; set; }
    public string? Result { get; set; }
    public string? Villain { get; set; }
    public string? Notes { get; set; }
    public int? HandNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
