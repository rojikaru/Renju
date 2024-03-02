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

    public virtual GameRound[] Rounds { get; set; } = rounds;
    public virtual GameRound CurrentRound { get; protected set; } = rounds[0];
    
    public virtual async Task Play()
    {
        foreach (var round in Rounds)
        {
            CurrentRound = round;
            await CurrentRound.Play();
        }
    }
}
