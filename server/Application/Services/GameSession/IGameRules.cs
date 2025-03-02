using server.Application.Services.GameSession.Dtos;
using server.Application.Services.Map.Dtos;

namespace server.Application.Services.GameSession;

public interface IGameRules
{
    GameResult CheckMap(GameBoard gameBoard);
}
