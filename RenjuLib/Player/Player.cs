namespace RenjuLib.Player;

public abstract class Player(
    bool isBlack,
    string name = "Player"
) : IPlayer
{
    public bool IsBlack => isBlack;
    public string Name { get; } = name;

    public abstract Task<Move> MakeMove();
}
