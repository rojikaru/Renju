namespace RenjuLib.Player;

public abstract class Player : IPlayer
{
    public CellStone Color { get; set; }
    public string Name { get; }
    
    /**
     * <summary>
     * <b>Constructs a player.</b> <br/>
     *
     * The player's color and name are set.
     * </summary>
     *
     * <param name="color">The player's color.</param>
     * <param name="name">The player's name.</param>
     */
    protected Player(CellStone color, string name = "Player")
    {
        if (color == CellStone.Empty)
            throw new ArgumentException("Color cannot be Empty.");
        
        Color = color;
        Name = name;
    }

    public abstract Task<Move> MakeMove(CancellationToken token = default);

    public abstract void ClearSubscriptions();
}
