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
        await CurrentRound.Play();
        GameEnded?.Invoke();
    }

    public override void Terminate()
    {
        CurrentRound.Terminate();
        OnTerminated?.Invoke();
    }

    public override ISession Clone() => new SingleSession(BlackPlayer, WhitePlayer);
}
