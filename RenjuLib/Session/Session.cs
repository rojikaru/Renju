namespace RenjuLib.Session;

/**
 * <summary>
 * The game session.
 * It contains the players, the game board, and rounds.
 * </summary>
 */
public abstract class Session(
    IPlayer blackPlayer,
    IPlayer whitePlayer,
    GameRound[] rounds
) : ISession
{
    public IPlayer BlackPlayer { get; } = blackPlayer;
    
    public IPlayer WhitePlayer { get; } = whitePlayer;

    public virtual IEnumerable<GameRound> Rounds { get; } = rounds;
    
    public virtual GameRound CurrentRound { get; protected set; } = rounds[0];

    public abstract Task Play();
    
    public virtual event Action? BoardChanged
    {
        add
        {
            foreach (var round in Rounds)
                round.RenjuBoard.BoardChanged += value;
        }
        remove
        {
            foreach (var round in Rounds)
                round.RenjuBoard.BoardChanged -= value;
        }
    }
}
