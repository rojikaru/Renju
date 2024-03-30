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
    
    public override GameResult Result => CurrentRound.Result;
    
    public override async Task Play()
    {
        await CurrentRound.Play();
        GameEnded?.Invoke();
    }
}
