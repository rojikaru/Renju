namespace RenjuLib.Player;

public interface IPlayer
{
    /**
     * The player's color.
     */
    bool IsBlack { get; }
    
    /**
     * The game session the player is attached to.
     */
    GameSession? Session { get; set; }
    
    /**
     * Await a move from the player.
     */
    Task<Move> MakeMove();
}
