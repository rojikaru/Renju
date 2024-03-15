namespace RenjuLib.Data;

/**
 * <summary>
 * <b>The move data.</b> <br/>
 *
 * It contains the stone info, player info,
 * and the coordinates of the move.
 * </summary>
 */
public sealed class Move : Intersection
{
    // TODO: Add player info
    
    private readonly CellStone _stone;

    public override CellStone Stone
    {
        get => _stone;
        init
        {
            if (value == CellStone.None)
                throw new ArgumentException("Stone cannot be None");
            
            _stone = value;
        }
    }
    
    public Move(int x, int y, CellStone stone)
    {
        X = x;
        Y = y;
        Stone = stone;
    }
}
