namespace RenjuLib.Player;

// TODO: Add stats for the player

/**
 * A player in the game.
 */
public interface IPlayer
{
    /**
     * The player's color.
     */
    bool IsBlack { get; }
    
    /**
     * The player's name.
     */
    string Name { get; }
    
    /**
     * Await a move from the player.
     */
    Task<Move> MakeMove();
}
