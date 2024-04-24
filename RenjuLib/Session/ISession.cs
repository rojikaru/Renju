namespace RenjuLib.Session;

/**
 * <summary>
 * The game session. <br/>
 * It contains the players, the game board, and rounds.
 * </summary>
 */
public interface ISession : ICloneable
{
    /**
     * <summary>
     * The black player.
     * </summary>
     */
    IPlayer BlackPlayer { get; }

    /**
     * <summary>
     * The white player.
     * </summary>
     */
    IPlayer WhitePlayer { get; }

    /**
     * <summary>
     * Start playing the game.
     * </summary>
     */
    Task Play();

    /**
     * <summary>
     * Terminate the game (with no winner).
     * </summary>
     */
    void Terminate();

    /**
     * <summary>
     * The event that is triggered when the board changes.
     * </summary>
     */
    event Action? BoardChanged;

    /**
     * <summary>
     * The event that is triggered when the game ends.
     * </summary>
     */
    event Action? GameEnded;

    /**
     * <summary>
     * The event that is triggered when the game is terminated.
     * </summary>
     */
    event Action? OnTerminated;

    /**
     * <summary>
     * The rounds of the game.
     * </summary>
     */
    IEnumerable<GameRound> Rounds { get; }

    /**
     * <summary>
     * The current round of the game.
     * </summary>
     */
    GameRound CurrentRound { get; }

    /**
     * <summary>
     * The result of the game.
     * </summary>
     */
    GameResult Result { get; }

    /**
     * <summary>
     * The board of the current game.
     * </summary>
     */
    RenjuBoard CurrentBoard { get; }

    /**
     * <summary>
     * Clone the session.
     * </summary>
     */
    new ISession Clone();

    object ICloneable.Clone() => Clone();
}
