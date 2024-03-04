namespace RenjuLib.Session;

public abstract class Session(
    IPlayer blackPlayer,
    IPlayer whitePlayer,
    GameRound[] rounds
) : ISession
{
    public IPlayer BlackPlayer { get; } = blackPlayer;
    public IPlayer WhitePlayer { get; } = whitePlayer;
    
    public RenjuBoard Board { get; } = new();

    public virtual GameRound[] Rounds { get; } = rounds;
    public virtual GameRound CurrentRound { get; } = rounds[0];

    public abstract Task Play();
}
