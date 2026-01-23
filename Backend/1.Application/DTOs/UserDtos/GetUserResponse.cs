namespace _1.Application.DTOs.UserDtos;

public class GetUserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}