namespace RenjuLib.Player;

public class HumanPlayer(
    CellStone color,
    string name
) : Player(color, name)
{
    public override async Task<Move> MakeMove()
        => await (
            AwaitMove?.Invoke()
            ?? throw new InvalidOperationException("AwaitMove is null")
        );

    /**
     * An event for handling human's clicks on the board.
     * Here should be the logic to await the player's input
     * and return the move they made.
     */
    public event Func<Task<Move>>? AwaitMove;
}
