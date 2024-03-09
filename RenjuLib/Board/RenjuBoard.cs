namespace RenjuLib.Board;

public class RenjuBoard
{
    public const int BoardSize = 13;
    
    public static bool IsWithinBounds(int x, int y)
    {
        return x is >= 0 and < BoardSize && y is >= 0 and < BoardSize;
    }

    /**
     * The board is 15x15, but the stone can be placed
     * only on the intersections of the lines.
     */
    public CellStone[,] Intersections { get; }
        = new CellStone[BoardSize, BoardSize];

    private void EnsureCellEmpty(int x, int y)
    {
        if (Intersections[x, y] != CellStone.None)
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
     * <exception cref="ArgumentOutOfRangeException">
     * Thrown when the move is out of the board.
     * </exception>
     * <exception cref="ArgumentException">
     * Thrown when the stone is None.
     * </exception>
     */
    public void AddMove(Move move)
    {
        // Ensuring the cell is empty
        EnsureCellEmpty(move.X, move.Y);

        // Ensuring the move is within the board is done by the Move class

        // Adding the move
        Intersections[move.X, move.Y] = move.Stone;
    }
}
