using System.Text.Json;
using server.Application.Services.GameSession.Dtos;

namespace server.Application.Extensions;

public static class GameSessionExtension
{
    public static GameSessionDto DeepCopy(this GameSessionDto dto)
    {
        var json = JsonSerializer.Serialize(dto);

        var copiedGame = JsonSerializer.Deserialize<GameSessionDto>(json);

        copiedGame.ActivePlayerConnection.WebSocket = dto.ActivePlayerConnection.WebSocket;

        foreach (var copiedPlayer in copiedGame.PlayerConnections)
        {
            foreach (var prevPlayer in dto.PlayerConnections)
            {
                if (copiedPlayer.User.Id == prevPlayer.User.Id)
                {
                    copiedPlayer.WebSocket = prevPlayer.WebSocket;
                    break;
                }
            }
        }

        return copiedGame;
    }
}
