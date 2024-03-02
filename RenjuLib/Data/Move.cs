namespace RenjuLib.Data;

public record struct Move
{
    private static void EnsureRange(int value, string name)
    {
        if (value is < 0 or > 13) 
            throw new ArgumentOutOfRangeException(name);
    }
    
    public bool IsBlack { get; set; }
    
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
    
    public Move(int x, int y, bool isBlack)
    {
        X = x;
        Y = y;
        IsBlack = isBlack;
    }
}
