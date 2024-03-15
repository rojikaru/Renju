namespace RenjuLib.Data;

/**
 * <summary>
 * <b>The intersection of the board.</b> <br/>
 *
 * Holds necessary data for the intersection display.
 * </summary>
 */
public class Intersection
{
    private const int BoardSize = 13;
    
    private static void EnsureRange(int value, string name)
    {
        if (value is < 0 or > BoardSize) 
            throw new ArgumentOutOfRangeException(name);
    }

    public virtual CellStone Stone { get; init; }
    
    private readonly int _x;
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
    public int Y
    {
        get => _y;
        init
        {
            EnsureRange(value, nameof(Y));
            _y = value;
        }
    }
    
    public Intersection(int x, int y, CellStone stone)
    {
        X = x;
        Y = y;
        Stone = stone;
    }
    
    protected Intersection() 
        : this(0, 0, CellStone.None) {}
}
