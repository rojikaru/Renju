namespace RenjuLib.Player;

/**
 * <summary>
 * A simple bot player.
 * </summary>
 */
public class SimpleBotPlayer(
    CellStone color,
    string name
) : Player(color, name)
{
    public SimpleBotPlayer() : this(CellStone.Empty, String.Empty) { }
    
    /**
     * Await a move from a bot.
     */
    public override async Task<Move> MakeMove(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
