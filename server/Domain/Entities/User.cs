using server.Infrastructure.Utils.Attributes;

namespace server.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    [Unique("This nickname is already in use")]
    public string Nickname { get; set; }
    public string Password { get; set; }
}
