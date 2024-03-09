namespace RenjuLib.Data;

public class Direction
{
    private int XOffset { get; set; }
    private int YOffset { get; set; }

    private Direction(int xOffset, int yOffset)
    {
        XOffset = xOffset;
        YOffset = yOffset;
    }

    public (int, int) MoveNext(int x, int y)
    {
        return (x + XOffset, y + YOffset);
    }

    // Define static members for commonly used directions:
    public static readonly Direction Up = new(0, -1);
    public static readonly Direction Down = new(0, 1);
    public static readonly Direction Right = new(1, 0);
    public static readonly Direction Left = new(-1, 0);
    public static readonly Direction DiagonalUpRight = new(1, -1);
    public static readonly Direction DiagonalDownRight = new(1, 1);
    public static readonly Direction DiagonalUpLeft = new(-1, -1);
    public static readonly Direction DiagonalDownLeft = new(-1, 1);
    
    public static readonly Direction[] AllDirections =
    [
        Up, Down, Right, Left,
        DiagonalUpRight, DiagonalDownRight,
        DiagonalUpLeft, DiagonalDownLeft
    ];
}
