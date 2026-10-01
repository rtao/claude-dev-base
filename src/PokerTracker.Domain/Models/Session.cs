namespace PokerTracker.Domain.Models;

public class Session
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public GameType GameType { get; set; }
    public string Stake { get; set; } = string.Empty;  // e.g. "1/2", "2/5"
    public decimal BuyIn { get; set; }
    public decimal CashOut { get; set; }
    public Currency Currency { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public decimal Profit => CashOut - BuyIn;
    public ICollection<HandHistory> Hands { get; set; } = new List<HandHistory>();
}
