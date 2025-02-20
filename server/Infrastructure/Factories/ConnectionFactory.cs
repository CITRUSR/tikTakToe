using Npgsql;

namespace server.Infrastructure.Factories;

public class ConnectionFactory(IConfiguration configuration) : IConnectionFactory
{
    private readonly IConfiguration _configuration = configuration;

    public NpgsqlConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("DbConnection");

        return new NpgsqlConnection(connectionString);
    }
}
