namespace _2.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; }
    public UserRole Role { get; set; } = UserRole.User;

    public ICollection<RefreshToken> Tokens { get; set; } = new List<RefreshToken>();
    public ICollection<WatchHistory> WatchHistories { get; set; }
        = new List<WatchHistory>();
}

public enum UserRole{
    User,
    Admin
}