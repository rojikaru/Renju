namespace RenjuLib.Player;

public class HumanPlayer(
    CellStone color,
    string name
) : Player(color, name)
{
    public override async Task<Move> MakeMove(CancellationToken token = default)
        => await (
            AwaitMove?.Invoke(token)
            ?? throw new InvalidOperationException("AwaitMove is null")
        );

    /**
     * An event for handling person's clicks on the board.
     * Here should be the logic to await the player's input
     * and return the move they made.
     */
    public event Func<CancellationToken, Task<Move>>? AwaitMove;
}
