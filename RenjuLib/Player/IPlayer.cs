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
    CellStone Color { get; set; }
    
    /**
     * Whether the player is black.
     */
    bool IsBlack => Color == CellStone.Black;
    
    /**
     * Whether the player is white.
     */
    bool IsWhite => Color == CellStone.White;
    
    /**
     * The player's name.
     */
    string Name { get; set; }
    
    /**
     * Await a move from the player.
     */
    Task<Move> MakeMove(CancellationToken cancellationToken = default);
}
