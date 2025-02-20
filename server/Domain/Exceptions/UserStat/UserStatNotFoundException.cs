namespace server.Domain.Exceptions.UserStat;

public class UserStatNotFoundException : NotFoundException
{
    public UserStatNotFoundException(Guid userId)
        : base($"Statistics for user with id:{userId} not found") { }
}
