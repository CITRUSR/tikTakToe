using server.Application.Services.Map.Dtos.Enums;
using server.Application.Services.User.Dtos.Responses;

namespace server.Application.Services.Map.Dtos;

public class Cell : ICloneable
{
    public CellState State { get; set; }
    public UserViewModel? User { get; set; }

    public object Clone()
    {
        return new Cell { State = State, User = User };
    }
}
