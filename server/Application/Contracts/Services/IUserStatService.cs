using server.Application.Services.UserStat.Dtos.Requests.GetUserStat;
using server.Application.Services.UserStat.Dtos.Requests.UpdateUserStat;
using server.Application.Services.UserStat.Dtos.Responses;

namespace server.Application.Contracts.Services;

public interface IUserStatService
{
    Task<UserStatDto> GetAsync(GetUserStatRequest request);
    Task<UserStatDto> UpdateAsync(UpdateUserStatRequest request);
}
