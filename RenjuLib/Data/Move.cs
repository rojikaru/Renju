namespace RenjuLib.Data;

/**
 * <summary>
 * <b>The move data.</b> <br/>
 *
 * It contains the stone info, player info,
 * and the coordinates of the move.
 * </summary>
 */
public sealed record Move(int X, int Y, CellStone Stone)
    : Intersection(X, Y, Stone)
{
    // TODO: Add player info

    private readonly CellStone _stone;
    
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
