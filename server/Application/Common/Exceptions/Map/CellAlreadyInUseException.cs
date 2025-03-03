using server.Application.Common.Exceptions.GameSession;

namespace server.Application.Common.Exceptions.Map;

public class CellAlreadyInUseException : SessionGameException
{
    public CellAlreadyInUseException(int x, int y)
        : base($"Cell with indexes x: {x} y:{y} already in use") { }
}
