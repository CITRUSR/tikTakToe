namespace server.Infrastructure.Utils.Constraints.Unique;

public interface IConstraintChecker
{
    void Check(Exception exception, Type type);
}
