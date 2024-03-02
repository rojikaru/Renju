namespace RenjuLib.Board;

public class RenjuBoard
{
    /**
     * The board is 15x15, but the stone can be placed
     * only on the intersections of the lines.
     */
    public Move?[,] Intersections { get; } = new Move?[13, 13];

    public GameResult Result { get; private set; }

    private void EnsureCellEmpty(int x, int y)
    {
        if (Intersections[x, y] is not null)
            throw new InvalidOperationException("Cell is already occupied");
    }

    /**
     * <summary>
     *  Add a move to the board.
     * </summary>
     * <param name="move">The move to add.</param>
     * <exception cref="InvalidOperationException">
     * Thrown when the cell is already occupied.
     * </exception>
     */
    public void AddMove(Move move)
    {
        EnsureCellEmpty(move.X, move.Y);
        Intersections[move.X, move.Y] = move;
    }
}
