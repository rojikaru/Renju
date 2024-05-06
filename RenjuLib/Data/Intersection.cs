namespace RenjuLib.Data;

/**
 * <summary>
 * <b>The intersection of the board.</b> <br/>
 *
 * Holds necessary data for the intersection display.
 * </summary>
 */
public record Intersection
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
    private const int BoardSize = 13;
    
    /**
     * <summary>
     * Check if the value is within the bounds of the board.
     * </summary>
     * <param name="value">The value to check.</param>
     * <param name="name">The name of the value.</param>
     * <exception cref="ArgumentOutOfRangeException">Thrown if the value is out of bounds.</exception>
     */
    private static void EnsureRange(int value, string name)
    {
        if (value is < 0 or >= BoardSize) 
            throw new ArgumentOutOfRangeException(name);
    }

    protected CellStone _stone;

    /**
     * <summary>
     * The stone that is placed on the intersection.
     * </summary>
     */
    public virtual CellStone Stone
    {
        get => _stone;
        init => _stone = value;
    }
    
    /**
     * <summary>
     * The emoji representation of the intersection.
     * </summary>
     */
    public string Emoji => Stone switch
    {
        CellStone.Black => "⚫",
        CellStone.White => "⚪",
        // _ => "⬜"
        _ => String.Empty,
    };
    
    private readonly int _x;
    /**
     * <summary>
     * The x coordinate of the intersection.
     * </summary>
     */
    public int X
    {
        get => _x;
        init
        {
            EnsureRange(value, nameof(X));
            _x = value;
        }
    }

    private readonly int _y;
    /**
     * <summary>
     * The y coordinate of the intersection.
     * </summary>
     */
    public int Y
    {
        get => _y;
        init
        {
            EnsureRange(value, nameof(Y));
            _y = value;
        }
    }
    
    /**
     * <summary>
     * Create a new intersection.
     * </summary>
     * <param name="x">The x coordinate of the intersection.</param>
     * <param name="y">The y coordinate of the intersection.</param>
     * <param name="stone">The stone that is placed on the intersection.</param>
     */
    public Intersection(int x, int y, CellStone stone)
    {
        X = x;
        Y = y;
        _stone = stone;
    }
    
    /**
     * <summary>
     * Create a new empty intersection.
     * </summary>
     */
    public Intersection() : this(0, 0, CellStone.Empty) { }
    
    public override string ToString() => $"Intersection({X}, {Y}, {Stone})";
}
