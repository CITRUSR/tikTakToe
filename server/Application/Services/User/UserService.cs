using Mapster;
using server.Application.Contracts.Repositories;
using server.Application.Contracts.Services;
using server.Application.Services.User.Dtos.Requests.GetUserById;
using server.Application.Services.User.Dtos.Requests.GetUserByNickname;
using server.Application.Services.User.Dtos.Requests.UpdateUser;
using server.Application.Services.User.Dtos.Responses;
using server.Domain.Exceptions.User;
using server.Infrastructure.Utils.Constraints.Unique;

namespace server.Application.Services.User;

public class UserService(IUnitOfWork unitOfWork, IUniqueConstraintChecker uniqueConstraintChecker)
    : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUniqueConstraintChecker _uniqueConstraintChecker = uniqueConstraintChecker;

    public async Task<List<UserViewModel>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();

        return users.Adapt<List<UserViewModel>>();
    }

    public async Task<UserDto> GetUserAsync(GetUserByIdRequest request)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(request.Id);

        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }

        return user.Adapt<UserDto>();
    }

    public async Task<UserDto> GetUserAsync(GetUserByNicknameRequest request)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(request.Nickname);

        if (user == null)
        {
            throw new UserNotFoundException(request.Nickname);
        }

        return user.Adapt<UserDto>();
    }

    public async Task<UserShortInfoDto> UpdateUserAsync(UpdateUserRequest request)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(request.Id);

        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }

        try
        {
            _unitOfWork.BeginTransaction();

            user.Nickname = request.Nickname;

            await _unitOfWork.UserRepository.UpdateAsync(user);

            _unitOfWork.CommitTransaction();
        }
        catch (Exception ex)
        {
            _unitOfWork.RollbackTransaction();

            _uniqueConstraintChecker.Check(ex, typeof(Domain.Entities.User));

            throw;
        }

        return user.Adapt<UserShortInfoDto>();
    }
}
