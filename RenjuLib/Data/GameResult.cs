namespace RenjuLib.Data;

/**
 * <summary>
 * <b>The result of the game.</b> <br/>
 *
 * It can be OnGoing, BlackWon, WhiteWon, or Draw.
 * </summary>
 */
public enum GameResult : byte
{
    /**
     * <summary>
     * The game is still ongoing.
     * </summary>
     */
    OnGoing,
    /**
     * <summary>
     * The black player won the game.
     * </summary>
     */
    BlackWon,
    /**
     * <summary>
     * The white player won the game.
     * </summary>
     */
    WhiteWon,
    /**
     * <summary>
     * The game ended in a draw.
     * </summary>
     */
    Draw
}
