namespace RenjuLib.Player;

public abstract class Player(CellStone color, string name) : IPlayer
{
    public CellStone Color { get; set; } = color;
    public string Name { get; set; } = name;
    
    protected Player() : this(CellStone.Empty, String.Empty) { }
    
    public abstract Task<Move> MakeMove(CancellationToken token = default);
}
