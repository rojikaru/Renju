namespace RenjuLib.Player;

public class SimpleBotPlayer(
    bool isBlack,
    string name
) : Player(isBlack, name)
{
    /**
     * Make a move as a bot.
     */
    public override async Task<Move> MakeMove()
    {
        throw new NotImplementedException();
    }
}
