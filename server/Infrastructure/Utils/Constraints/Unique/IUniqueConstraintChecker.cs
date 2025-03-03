using server.Domain.Exceptions;

namespace server.Infrastructure.Utils.Constraints.Unique;

public interface IUniqueConstraintChecker : IConstraintChecker
{
    AlreadyExistsException? Exception { get; }
}
