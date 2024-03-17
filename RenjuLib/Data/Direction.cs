namespace RenjuLib.Data;

/**
 * <summary>
 * <b>The direction of the move.</b> <br/>
 *
 * It contains the x and y offset of the move.
 * </summary>
 */
public class Direction
{
    /**
     * <summary>
     * The x offset of the direction.
     * </summary>
     */
    private int XOffset { get; }
    
    /**
     * <summary>
     * The y offset of the direction.
     * </summary>
     */
    private int YOffset { get; }

    /**
     * <summary>
     * Create a new direction.
     * </summary>
     * <param name="xOffset">The x offset of the direction.</param>
     * <param name="yOffset">The y offset of the direction.</param>
     */
    private Direction(int xOffset, int yOffset)
    {
        XOffset = xOffset;
        YOffset = yOffset;
    }

    /**
     * <summary>
     * Move to the next intersection.
     * </summary>
     * <param name="x">The x coordinate of the current intersection.</param>
     * <param name="y">The y coordinate of the current intersection.</param>
     * <returns>The new coordinates of the intersection.</returns>
     */
    public (int, int) MoveNext(int x, int y)
    {
        return (x + XOffset, y + YOffset);
    }

    #region CommonlyUsedDirections
    
    /**
     * <summary>
     * The up direction.
     * </summary>
     */
    public static readonly Direction Up = new(0, -1);
    
    /**
     * <summary>
     * The down direction.
     * </summary>
     */
    public static readonly Direction Down = new(0, 1);
    
    /**
     * <summary>
     * The right direction.
     * </summary>
     */
    public static readonly Direction Right = new(1, 0);
    
    /**
     * <summary>
     * The left direction.
     * </summary>
     */
    public static readonly Direction Left = new(-1, 0);
    
    /**
     * <summary>
     * The diagonal up-right direction.
     * </summary>
     */
    public static readonly Direction DiagonalUpRight = new(1, -1);
    
    /**
     * <summary>
     * The diagonal down-right direction.
     * </summary>
     */
    public static readonly Direction DiagonalDownRight = new(1, 1);
    
    /**
     * <summary>
     * The diagonal up-left direction.
     * </summary>
     */
    public static readonly Direction DiagonalUpLeft = new(-1, -1);
    
    /**
     * <summary>
     * The diagonal down-left direction.
     * </summary>
     */
    public static readonly Direction DiagonalDownLeft = new(-1, 1);
    
    #endregion
    
    /**
     * <summary>
     * All possible directions.
     * </summary>
     */
    public static readonly Direction[] AllDirections =
    [
        Up, Down, Right, Left,
        DiagonalUpRight, DiagonalDownRight,
        DiagonalUpLeft, DiagonalDownLeft
    ];
}
