namespace RenjuLib.Player;

public class HumanPlayer(
    CellStone color,
    string name
) : Player(color, name)
{
    public HumanPlayer() : this(CellStone.Empty, String.Empty) { }
    
    public override async Task<Move> MakeMove(CancellationToken token = default)
        => await (
            AwaitMoveInternal?.Invoke(token)
            ?? throw new InvalidOperationException("AwaitMove is null")
        );

    private readonly List<Func<CancellationToken, Task<Move>>> _moveActions = [];
    private event Func<CancellationToken, Task<Move>>? AwaitMoveInternal;

    /**
     * An event for handling person's clicks on the board.
     * Here should be the logic to await the player's input
     * and return the move they made.
     */
    public event Func<CancellationToken, Task<Move>>? AwaitMove
    {
        add
        {
            ArgumentNullException.ThrowIfNull(value);

            AwaitMoveInternal += value;
            _moveActions.Add(value);
        }
        remove
        {
            ArgumentNullException.ThrowIfNull(value);

            AwaitMoveInternal -= value;
            _moveActions.Remove(value);
        }
    }

    public void ClearSubscriptions()
    {
        foreach (Func<CancellationToken, Task<Move>> action in _moveActions)
            AwaitMoveInternal -= action;

        _moveActions.Clear();
    }
}
