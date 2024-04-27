namespace RenjuLib.Session;

/**
 * <summary>
 * The game session.
 * It contains the players, the game board, and rounds.
 * </summary>
 */
public abstract class Session : ISession
{
    protected Session(
        IPlayer blackPlayer,
        IPlayer whitePlayer,
        IEnumerable<GameRound> rounds
    )
    {
        GameRound[] gameRounds = rounds as GameRound[] ?? rounds.ToArray();
        Rounds = gameRounds;
        CurrentRound = gameRounds[0];

        blackPlayer.Color = CellStone.Black;
        whitePlayer.Color = CellStone.White;

        BlackPlayer = blackPlayer;
        WhitePlayer = whitePlayer;
    }

    public IPlayer BlackPlayer { get; }

    public IPlayer WhitePlayer { get; }

    public virtual IEnumerable<GameRound> Rounds { get; }

    public GameRound CurrentRound { get; protected set; }

    public virtual RenjuBoard CurrentBoard => CurrentRound.RenjuBoard;

    public abstract event Action? GameEnded;

    public abstract event Action? OnTerminated;

    public abstract GameResult Result { get; }

    public abstract Task Play();

    public abstract Task Terminate();

    public abstract ISession Clone();

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

    /**
     * <summary>
     * The default action when the game ends
     * (shows a message box with the result).
     * </summary>
     */
    public static Action AlertOnGameEnded(
        ISession session,
        IMessageService messageService
    ) => () =>
    {
        if (session.Result == GameResult.Cancelled) return;

        messageService.ShowAsync(
            "Game over!",
            session.Result switch
            {
                GameResult.BlackWon => "Black player wins!",
                GameResult.WhiteWon => "White player wins!",
                GameResult.Draw => "It's a draw!",
                _ => "Unknown result"
            },
            "OK"
        );
    };
}
