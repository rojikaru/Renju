namespace RenjuLib.Player;

public class SimpleBotPlayer(
    CellStone color,
    string name
) : Player(color, name)
{
    /**
     * Make a move as a bot.
     */
    public override async Task<Move> MakeMove()
    {
        throw new NotImplementedException();
    }
}
