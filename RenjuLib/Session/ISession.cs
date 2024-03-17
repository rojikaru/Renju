namespace RenjuLib.Session;

/**
 * <summary>
 * The game session. <br/>
 * It contains the players, the game board, and rounds.
 * </summary>
 */
public interface ISession
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
     * The event that is triggered when the board changes.
     * </summary>
     */
    event Action? BoardChanged;
    
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
}
