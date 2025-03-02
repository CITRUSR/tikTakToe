using server.Application.Common.Exceptions.Map;
using server.Application.Services.Map.Dtos.Enums;
using server.Application.Services.User.Dtos.Responses;

namespace server.Application.Services.Map.Dtos;

public class GameBoard
{
    public Guid GameId { get; set; }

    private readonly Cell[,] _map =
    {
        { new(), new(), new() },
        { new(), new(), new() },
        { new(), new(), new() },
    };

    public GameBoard() { }

    public GameBoard(List<List<Cell>> map)
    {
        CheckBorders(map.Count, map[0].Count);

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                _map[i, j] = (Cell)map[i][j].Clone();
            }
        }
    }

    public Cell this[int row, int column]
    {
        get
        {
            CheckBorders(row, column);

            var c = (Cell)_map[row, column].Clone();

            return c;
        }
    }

    public Cell SetCellState(int x, int y, CellState state, UserViewModel user)
    {
        CheckBorders(x, y);

        if (_map[x, y].User != null)
        {
            throw new CellAlreadyInUseException(x, y);
        }

        _map[x, y].State = state;
        _map[x, y].User = user;

        return (Cell)_map[x, y].Clone();
    }

    public bool IsFull()
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (_map[i, j].State == CellState.Empty)
                    return false;
            }
        }

        return true;
    }

    private void CheckBorders(int x, int y)
    {
        if (x < 0 || x >= _map.Length || y < 0 || y >= _map.Length)
        {
            throw new MapOutOfRangeException();
        }
    }
}
