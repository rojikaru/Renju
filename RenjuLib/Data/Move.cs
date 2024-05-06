namespace RenjuLib.Data;

/**
 * <summary>
 * <b>The move data.</b> <br/>
 *
 * It contains the stone info, player info,
 * and the coordinates of the move.
 * </summary>
 */
public sealed record Move : Intersection
{
    public Move(int x, int y, CellStone stone)
    {
        X = x;
        Y = y;
        Stone = stone;
    }
    
    // TODO: Add player info

    public override CellStone Stone
    {
        get => _stone;
        init
        {
            if (value == CellStone.Empty)
                throw new ArgumentException("Stone cannot be Empty");

            _stone = value;
        }
    }

    public override string ToString() => $"Move({X}, {Y}, {Stone})";
}
