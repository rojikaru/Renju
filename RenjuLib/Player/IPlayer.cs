namespace RenjuLib.Player;

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
     * The current game session the player is attached to.
     */
    ISession Session { get; set; }
    
    /**
     * Await a move from the player.
     */
    Task<Move> MakeMove();
}
