namespace RenjuLib.Data;

/**
 * <summary>
 * <b>The stone of the cell.</b> <br/>
 *
 * It can be Empty, Black, or White.
 * </summary>
 */
public enum CellStone : byte
{
    /**
     * <summary>
     * The cell is empty.
     * </summary>
     */
    Empty,
    /**
     * <summary>
     * The cell is occupied by a black stone.
     * </summary>
     */
    Black,
    /**
     * <summary>
     * The cell is occupied by a white stone.
     * </summary>
     */
    White
}
