using server.Application.Services.GameSession.Dtos;
using server.Application.Services.Map.Dtos;
using server.Application.Services.Map.Dtos.Enums;

namespace server.Application.Services.GameSession;

public class GameRules : IGameRules
{
    public GameResult CheckMap(GameBoard gameBoard)
    {
        GameResult result = new GameResult();

        //rows check and columns check
        for (var i = 0; i < 3; i++)
        {
            Guid? winnerId = null;

            if (CheckLine(gameBoard[i, 0], gameBoard[i, 1], gameBoard[i, 2], out winnerId))
            {
                result.WinnerId = winnerId;

                return result;
            }

            if (CheckLine(gameBoard[0, i], gameBoard[1, i], gameBoard[2, i], out winnerId))
            {
                result.WinnerId = winnerId;

                return result;
            }
        }

        //diagonals check
        var diagCell = gameBoard[1, 1];
        if (diagCell.State != CellState.Empty)
        {
            Guid? winnerId = null;

            if (CheckLine(diagCell, gameBoard[0, 0], gameBoard[2, 2], out winnerId))
            {
                result.WinnerId = winnerId;

                return result;
            }
            if (CheckLine(diagCell, gameBoard[0, 2], gameBoard[2, 0], out winnerId))
            {
                result.WinnerId = winnerId;

                return result;
            }
        }

        if (gameBoard.IsFull())
        {
            result.IsDraw = true;

            return result;
        }

        return result;
    }

    private bool CheckLine(Cell cell1, Cell cell2, Cell cell3, out Guid? winnerId)
    {
        winnerId = null;

        var cell = cell1;
        if (cell.State != CellState.Empty && cell2.State == cell.State && cell3.State == cell.State)
        {
            winnerId = cell.User.Id;
            return true;
        }

        return false;
    }
}
