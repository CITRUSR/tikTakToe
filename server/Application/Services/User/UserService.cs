using Mapster;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.User.Dtos.Responses;
using server.Domain.Exceptions.User;

namespace server.Application.Services.User;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<List<UserViewModel>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();

        return users.Adapt<List<UserViewModel>>();
    }

    public async Task<UserDto> GetUserAsync(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(id);

        if (user == null)
        {
            throw new UserNotFoundException(id);
        }

        return user.Adapt<UserDto>();
    }

    public async Task<UserDto> GetUserAsync(string Nickname)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(Nickname);

        if (user == null)
        {
            throw new UserNotFoundException(Nickname);
        }

        return user.Adapt<UserDto>();
    }
}
