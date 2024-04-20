namespace RenjuLib.Session;

/**
 * <summary>
 * The game session.
 * It contains the players, the game board, and rounds.
 * </summary>
 */
public abstract class Session : ISession
{
    public Session(
    IPlayer blackPlayer,
    IPlayer whitePlayer,
    GameRound[] rounds
)
    {
        Rounds = rounds;
        CurrentRound = rounds[0];

        blackPlayer.Color = CellStone.Black;
        whitePlayer.Color = CellStone.White;

        BlackPlayer = blackPlayer;
        WhitePlayer = whitePlayer;
    }

    public IPlayer BlackPlayer { get; }
    
    public IPlayer WhitePlayer { get; }

    public virtual IEnumerable<GameRound> Rounds { get; }
    
    public virtual GameRound CurrentRound { get; protected set; }
    
    public virtual RenjuBoard CurrentBoard => CurrentRound.RenjuBoard;

    public abstract event Action? GameEnded;
    
    public abstract GameResult Result { get; }

    public abstract Task Play();
    
    public virtual event Action? BoardChanged
    {
        add
        {
            foreach (GameRound? round in Rounds)
                round.RenjuBoard.BoardChanged += value;
        }
        remove
        {
            foreach (GameRound? round in Rounds)
                round.RenjuBoard.BoardChanged -= value;
        }
    }
}
