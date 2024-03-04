namespace RenjuLib.Board;

public class RenjuBoard
{
    private const int BoardSize = 13;
    
    /**
     * The board is 15x15, but the stone can be placed
     * only on the intersections of the lines.
     */
    public CellStone[,] Intersections { get; } 
        = new CellStone[BoardSize, BoardSize];

    public GameResult Result { get; private set; }

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
     */
    public void AddMove(Move move)
    {
        EnsureCellEmpty(move.X, move.Y);
        Intersections[move.X, move.Y] = move.Stone;
    }
}
