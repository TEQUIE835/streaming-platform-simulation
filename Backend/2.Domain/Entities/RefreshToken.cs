namespace _2.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(30);
    public bool IsExpired => DateTime.UtcNow > Expires;
    public Guid UserId { get; set; }
    public User User { get; set; }
}