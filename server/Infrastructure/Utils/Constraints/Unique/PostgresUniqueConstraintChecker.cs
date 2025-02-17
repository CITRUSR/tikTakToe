using Npgsql;
using server.Domain.Exceptions;
using server.Infrastructure.Utils.Attributes;

namespace server.Infrastructure.Utils.Constraints.Unique;

public class PostgresUniqueConstraintChecker : IUniqueConstraintChecker
{
    private AlreadyExistsException? _exception = null;
    public AlreadyExistsException? Exception => _exception;

    public void Check(Exception exception, Type type)
    {
        if (exception is PostgresException && exception.Message.Contains("23505"))
        {
            var (property, uniqueConstraint) = type.GetProperties()
                .Select(x => new
                {
                    Property = x,
                    UniqueConstraint = x.GetCustomAttributes(typeof(UniqueAttribute), true)
                        .FirstOrDefault(),
                })
                .Where(x => x.UniqueConstraint != null)
                .Select(x => (x.Property, (UniqueAttribute)x.UniqueConstraint!))
                .FirstOrDefault();

            if (property == null)
                _exception = new AlreadyExistsException(exception.Message);

            _exception = new AlreadyExistsException(uniqueConstraint.ErrorMessage);
        }
    }
}
