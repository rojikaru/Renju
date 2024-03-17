namespace RenjuLib.Player;

// TODO: Add an event for when the player clicks on the board

public class HumanPlayer(
    CellStone color,
    string name
) : Player(color, name)
{
    /**
     * Await a move from the player.
     */
    public override async Task<Move> MakeMove()
        => await (
            AwaitMove?.Invoke()
            ?? throw new InvalidOperationException("AwaitMove is null")
        );

    public event Func<Task<Move>>? AwaitMove;
}
