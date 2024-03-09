namespace RenjuLib.Round;

public class GameRound(
    IPlayer blackPlayer,
    IPlayer whitePlayer
)
{
    private RenjuBoard RenjuBoard { get; } = new();

    public GameResult Result { get; private set; }

    private bool IsWin(CellStone stoneColor, Move move)
    {
        if (stoneColor == CellStone.None)
            throw new ArgumentException("Invalid stone color", nameof(stoneColor));

        // Check for a win in all directions (rows, columns, diagonals)
        foreach (var direction in Direction.AllDirections)
            if (HasFiveInARow(move.X, move.Y, direction, stoneColor))
                return true;

        return false;
    }

    private bool HasFiveInARow(
        int x, int y,
        Direction direction,
        CellStone stoneToCheck
    )
    {
        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            if (RenjuBoard.IsWithinBounds(x, y) &&
                RenjuBoard.Intersections[x, y] == stoneToCheck)
                count++;
            else break;

            (x, y) = direction.MoveNext(x, y);
        }

        return count >= 5;
    }

    private bool IsDraw()
    {
        // Check if the board is full
        for (int i = 0; i < RenjuBoard.BoardSize; i++)
        for (int j = 0; j < RenjuBoard.BoardSize; j++)
            if (RenjuBoard.Intersections[i, j] == CellStone.None)
                // Board is not full, so it's not a draw
                return false;

        // Board is full, so it's a draw
        return true;
    }

    private async Task<GameResult> MakeTurn(IPlayer player)
    {
        try
        {
            var move = await player.MakeMove();
            RenjuBoard.AddMove(move);

            if (IsWin(player.Color, move))
                return player.IsBlack ? GameResult.BlackWin : GameResult.WhiteWin;
            // else
            return IsDraw() ? GameResult.Draw : GameResult.OnGoing;
        }
        catch
        {
            // On error, show the error and ask for another move
            // OnError?.Invoke(player, e);
            return await MakeTurn(player);
        }
    }

    public async Task<GameResult> Play()
    {
        bool isBlack = true;
        while (true)
        {
            var result = await MakeTurn(isBlack ? blackPlayer : whitePlayer);
            if (result != GameResult.OnGoing)
            {
                Result = result;
                return result;
            }

            isBlack = !isBlack;
        }
    }
}
