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
    private bool _started = false;
    
    public override event Action? GameEnded;

    public override event Action? OnTerminated;

    public override GameResult Result => CurrentRound.Result;

    public override async Task Play()
    {
        if (_started)
            throw new InvalidOperationException("Game has already started");
        
        _started = true;
        
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
