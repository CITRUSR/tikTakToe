using server.Application.Services.Map.Dtos;

namespace server.Application.Extensions;

public static class GameBoardExtension
{
    public static List<List<Cell>> GetJaggedArray(this GameBoard gameBoard)
    {
        var board = new List<List<Cell>>();

        for (int i = 0; i < 3; i++)
        {
            var row = new List<Cell>();
            for (int j = 0; j < 3; j++)
            {
                row.Add(gameBoard[i, j]);
            }
            board.Add(row);
        }
        return board;
    }

    public static GameBoard DeepCopy(this GameBoard? gameBoard)
    {
        if (gameBoard == null)
            return gameBoard;

        var newBoard = new GameBoard(gameBoard.GetJaggedArray()) { GameId = gameBoard.GameId };

        return newBoard;
    }
}
