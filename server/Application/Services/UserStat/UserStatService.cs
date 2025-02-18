using Mapster;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.UserStat.Dtos.Requests.GetUserStat;
using server.Application.Services.UserStat.Dtos.Requests.UpdateUserStat;
using server.Application.Services.UserStat.Dtos.Responses;
using server.Domain.Exceptions.UserStat;

namespace server.Application.Services.UserStat;

public class UserStatService(IUnitOfWork unitOfWork) : IUserStatService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<UserStatDto> GetAsync(GetUserStatRequest request)
    {
        var userStat = await _unitOfWork.UserStatRepository.GetAsync(request.UserId);

        if (userStat == null)
        {
            throw new UserStatNotFoundException(request.UserId);
        }

        return userStat.Adapt<UserStatDto>();
    }
}
