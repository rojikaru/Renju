using System.Collections.ObjectModel;

namespace RenjuLib.Board;

public class RenjuBoard
{
    /*
     * The size of the renju board.
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
    {
        return x is >= 0 and < BoardSize && y is >= 0 and < BoardSize;
    }

    public RenjuBoard()
    {
        Intersections = [];
        
        for (var x = 0; x < BoardSize; x++)
            for (var y = 0; y < BoardSize; y++)
                Intersections.Add(new Intersection(x, y, CellStone.None));
    }

    /**
     * The board is 15x15, but the stone can be placed
     * only on the intersections of the lines.
     */
    public ObservableCollection<Intersection> Intersections { get; }
    
    /**
     * <summary>
     * Get a data of a specified cell status.
     * </summary>
     * <param name="x">The x coordinate of the cell.</param>
     * <param name="y">The y coordinate of the cell.</param>
     * <returns>The data of the cell.</returns>
     */
    public Intersection CellAt(int x, int y) => Intersections[x * BoardSize + y];

    /**
     * <summary>
     * Check if the cell is empty.
     * </summary>
     * <param name="x">The x coordinate of the cell.</param>
     * <param name="y">The y coordinate of the cell.</param>
     * <returns>True if the cell is empty, false otherwise.</returns>
     */
    public bool IsCellEmpty(int x, int y) => CellAt(x, y).Stone == CellStone.None;

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
        // Ensuring the move is within the board is done by the Move class
        // Ensuring the cell is empty
        if (IsCellEmpty(move.X, move.Y))
            throw new InvalidOperationException("Cell is already occupied");

        // Adding the move
        Intersections[move.X * BoardSize + move.Y] = move;
    }
    
    // TODO: Consider adding a RevertMove method
}
