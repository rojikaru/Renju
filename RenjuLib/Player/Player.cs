namespace RenjuLib.Player;

public abstract class Player(
    bool isBlack,
    string name = "Player",
    ISession session = null!
) : IPlayer
{
    public bool IsBlack => isBlack;
    public string Name { get; } = name;
    public ISession Session { get; set; } = session;

    public abstract Task<Move> MakeMove();
}
