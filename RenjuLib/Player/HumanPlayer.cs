namespace RenjuLib.Player;

public class HumanPlayer(
    bool isBlack,
    GameSession? session = null
) : Player(isBlack, session)
{
    public override async Task<Move> MakeMove()
    {
        throw new NotImplementedException();
    }
}
