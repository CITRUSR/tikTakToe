using server.Application.Services.User.Dtos.Requests.GetUserById;
using server.Application.Services.User.Dtos.Requests.GetUserByNickname;
using server.Application.Services.User.Dtos.Requests.UpdateUser;
using server.Application.Services.User.Dtos.Responses;

namespace server.Application.Contracts.Services;

public interface IUserService
{
    Task<List<UserViewModel>> GetAllUsersAsync();
    Task<UserDto> GetUserAsync(GetUserByIdRequest request);
    Task<UserDto> GetUserAsync(GetUserByNicknameRequest request);
    Task<UserShortInfoDto> UpdateUserAsync(UpdateUserRequest request);
}
