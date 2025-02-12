using server.Application.Services.User.Dtos.Responses;

namespace server.Application.Contracts.Services;

public interface IUserService
{
    Task<List<UserViewModel>> GetAllUsersAsync();
}
