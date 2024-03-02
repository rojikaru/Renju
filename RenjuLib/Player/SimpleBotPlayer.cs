namespace RenjuLib.Player;

public class SimpleBotPlayer(
    bool isBlack,
    string name,
    ISession session = null!
) : Player(isBlack, name, session)
{
    /**
     * Make a move as a bot.
     */
    public override async Task<Move> MakeMove()
    {
        throw new NotImplementedException();
    }
}
