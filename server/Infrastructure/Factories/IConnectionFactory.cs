using Npgsql;

namespace server.Infrastructure.Factories;

public interface IConnectionFactory
{
    NpgsqlConnection CreateConnection();
}
