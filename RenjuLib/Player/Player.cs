namespace RenjuLib.Player;

public abstract class Player : IPlayer
{
    public CellStone Color { get; }
    public string Name { get; }
    
    protected Player(CellStone color, string name = "Player")
    {
        if (color == CellStone.None)
            throw new ArgumentException("Color cannot be None.");
        
        Color = color;
        Name = name;
    }

    public abstract Task<Move> MakeMove();
}
