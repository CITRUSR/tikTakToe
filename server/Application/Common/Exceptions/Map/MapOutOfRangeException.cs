using server.Application.Common.Exceptions.GameSession;

namespace server.Application.Common.Exceptions.Map;

public class MapOutOfRangeException : SessionGameException
{
    public MapOutOfRangeException()
        : base("Map has 3x3 size") { }
}
