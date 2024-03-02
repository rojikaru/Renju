namespace RenjuLib.Player;

public abstract class Player(
    bool isBlack,
    GameSession? session = null
) : IPlayer
{
    public bool IsBlack => isBlack;
    public GameSession? Session { get; set; } = session;

    public abstract Task<Move> MakeMove();
}
