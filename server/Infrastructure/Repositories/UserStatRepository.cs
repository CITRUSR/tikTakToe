using Dapper;
using server.Application.Contracts.Repositories;
using server.Domain.Entities;
using server.Infrastructure.Factories;
using server.Infrastructure.Sql.Queries;

namespace server.Infrastructure.Repositories;

public class UserStatRepository(IConnectionFactory connectionFactory) : IUserStatRepository
{
    private readonly IConnectionFactory _connectionFactory = connectionFactory;
}
