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
        var userStat = await GetUserStatAsync(request.UserId);

        return userStat.Adapt<UserStatDto>();
    }

    public async Task<UserStatDto> UpdateAsync(UpdateUserStatRequest request)
    {
        var userStat = await GetUserStatAsync(request.UserId);

        userStat.Wins = request.Wins;
        userStat.Losses = request.Losses;
        userStat.GamesCount = request.GamesCount;

        await _unitOfWork.UserStatRepository.UpdateAsync(userStat);

        return userStat.Adapt<UserStatDto>();
    }

    private async Task<Domain.Entities.UserStat> GetUserStatAsync(Guid id)
    {
        var userStat = await _unitOfWork.UserStatRepository.GetAsync(id);

        if (userStat == null)
        {
            throw new UserStatNotFoundException(id);
        }

        return userStat;
    }
}
