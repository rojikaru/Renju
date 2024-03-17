namespace RenjuLib.Session;

public class SingleSession(
    IPlayer blackPlayer,
    IPlayer whitePlayer
) : Session(
    blackPlayer,
    whitePlayer,
    [new GameRound(blackPlayer, whitePlayer)]
)
{
    public override async Task Play() => await CurrentRound.Play();
}
