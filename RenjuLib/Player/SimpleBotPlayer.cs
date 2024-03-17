namespace RenjuLib.Player;

public class SimpleBotPlayer(
    CellStone color,
    string name
) : Player(color, name)
{
    /**
     * Await a move from a bot.
     */
    public override async Task<Move> MakeMove()
    {
        throw new NotImplementedException();
    }
}
