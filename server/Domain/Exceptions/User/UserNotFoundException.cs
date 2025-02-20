using Microsoft.AspNetCore.Http.HttpResults;

namespace server.Domain.Exceptions.User;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid id)
        : base($"User with id: {id} not found") { }

    public UserNotFoundException(string nickname)
        : base($"User with nickname: {nickname} not found") { }
}
