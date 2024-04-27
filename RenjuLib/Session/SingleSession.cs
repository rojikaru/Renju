namespace RenjuLib.Session;

/**
 * <summary>
 * The game session for a single round.
 * </summary>
 */
public class SingleSession(
    IPlayer blackPlayer,
    IPlayer whitePlayer
) : Session(
    blackPlayer,
    whitePlayer,
    [new GameRound(blackPlayer, whitePlayer)]
)
{
    public override event Action? GameEnded;

    public override event Action? OnTerminated;

    public override GameResult Result => CurrentRound.Result;

    public override async Task Play()
    {
        GameResult result = await CurrentRound.Play();
        if (result != GameResult.Cancelled)
            GameEnded?.Invoke();
    }

    public override async Task Terminate()
    {
        await CurrentRound.Terminate();
        OnTerminated?.Invoke();
    }

    public override ISession Clone() => new SingleSession(BlackPlayer, WhitePlayer);
}
