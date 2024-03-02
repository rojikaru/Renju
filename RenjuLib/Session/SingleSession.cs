namespace RenjuLib.Session;

public class SingleSession(
    IPlayer blackPlayer,
    IPlayer whitePlayer
) : Session(blackPlayer, whitePlayer, new GameRound[1])
{
    public override async Task Play()
    {
        await CurrentRound.Play();
    }
}
