using _1.Application.DTOs.UserDtos;
using _1.Application.Interfaces.UserInterfaces;
using _2.Domain.Entities;

namespace _1.Application.Services.UserServices;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ICollection<GetUserResponse>?> GetAllUsersAsync()
    {
        var users = await _userRepository.GetUsers();
        var usersResponse = new List<GetUserResponse>();
        foreach (var user in users){
            if(user.Role.ToString() == "User"){
                usersResponse.Add(new GetUserResponse()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Username,
                    Role = user.Role.ToString(),
                });
            }
        }
        return usersResponse;
    }
}