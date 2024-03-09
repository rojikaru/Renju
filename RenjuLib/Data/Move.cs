namespace RenjuLib.Data;

public record struct Move
{
    private const int BoardSize = 13;
    
    private static void EnsureRange(int value, string name)
    {
        if (value is < 0 or > BoardSize) 
            throw new ArgumentOutOfRangeException(name);
    }

    private readonly CellStone _stone;

    public CellStone Stone
    {
        get => _stone;
        init
        {
            if (value == CellStone.None)
                throw new ArgumentException("Stone cannot be None");
            
            _stone = value;
        }
    }
    
    public bool IsBlack => Stone == CellStone.Black;
    public bool IsWhite => Stone == CellStone.White;
    
    private readonly int _x;
    public int X
    {
        get => _x;
        init
        {
            EnsureRange(value, nameof(X));
            _x = value;
        }
    }
    
    private readonly int _y;
    public int Y
    {
        get => _y;
        init
        {
            EnsureRange(value, nameof(Y));
            _y = value;
        }
    }
    
    public Move(int x, int y, CellStone stone)
    {
        X = x;
        Y = y;
        Stone = stone;
    }
}
