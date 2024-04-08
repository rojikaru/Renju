namespace RenjuLib.Board;

/**
 * <summary>
 * The renju board.
 * </summary>
 */
public class RenjuBoard
{
    /**
     * <summary>
     * The size of the renju board.
     * </summary>
     * <remarks>
     * The board is 15x15, but the stone can be placed
     * only on the intersections of the lines.
     * </remarks>
     */
    public const int BoardSize = 13;

    /**
     * <summary>
     * Check if the coordinates are within the bounds of the board.
     * </summary>
     * <param name="x">The x coordinate.</param>
     * <param name="y">The y coordinate.</param>
     * <returns>True if the coordinates are within the bounds, false otherwise.</returns>
     */
    public static bool IsWithinBounds(int x, int y)
        => x is >= 0 and < BoardSize && y is >= 0 and < BoardSize;

    /**
     * <summary>
     * The event that is raised when the board is changed.
     * </summary>
     */
    public event Action? BoardChanged;

    /**
     * <summary>
     * The intersections of the board. (Flat list)
     * </summary>
     * <remarks>
     * The board is 15x15, but the stone can be placed
     * only on the intersections of the lines.
     * </remarks>
     */
    public ObservableCollection<Intersection> Intersections { get; } = [];

    /**
     * <summary>
     * Create a new instance of the RenjuBoard class.
     * </summary>
     */
    public RenjuBoard()
    {
        for (int x = 0; x < BoardSize; x++)
        for (int y = 0; y < BoardSize; y++)
            Intersections.Add(new Intersection(x, y, CellStone.Empty));
    }

    /**
     * <summary>
     * Get a a specified cell.
     * </summary>
     * <param name="x">The x coordinate of the cell.</param>
     * <param name="y">The y coordinate of the cell.</param>
     * <returns>The cell on a requested position.</returns>
     */
    public Intersection this[int x, int y] => Intersections[x * BoardSize + y];

    /**
     * <summary>
     * Check if the cell is empty.
     * </summary>
     * <param name="x">The x coordinate of the cell.</param>
     * <param name="y">The y coordinate of the cell.</param>
     * <returns>True if the cell is empty, false otherwise.</returns>
     */
    public bool IsCellEmpty(int x, int y)
        => this[x, y].Stone == CellStone.Empty;

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
     * Thrown when the stone is Empty.
     * </exception>
     */
    public void AddMove(Move move)
    {
        // Ensuring the move is within the board is done by the Move class

        // Ensuring the cell is empty
        if (!IsCellEmpty(move.X, move.Y))
            throw new InvalidOperationException("Cell is already occupied");

        // Adding the move
        Intersections[move.X * BoardSize + move.Y] = move;

        // Raising the event that the board has changed
        BoardChanged?.Invoke();
    }

    // TODO: Consider adding a RevertMove method to revert the last move
}
