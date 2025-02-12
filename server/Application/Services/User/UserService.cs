using Mapster;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.User.Dtos.Responses;

namespace server.Application.Services.User;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<UserViewModel>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();

        return users.Adapt<List<UserViewModel>>();
    }
}
