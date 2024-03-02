namespace RenjuLib.Session;

public class GameSession(
    IPlayer blackPlayer,
    IPlayer whitePlayer
)
{
    public RenjuBoard RenjuBoard { get; } = new();

    private bool IsWin(bool isBlack)
    {
        throw new NotImplementedException();
    }
    
    private bool IsDraw()
    {
        throw new NotImplementedException();
    }
    
    private async Task<GameResult> MakeTurn(IPlayer player)
    {
        try
        {
            var move = await player.MakeMove();
            RenjuBoard.AddMove(move);
            
            if (IsWin(player.IsBlack))
                return player.IsBlack ? GameResult.BlackWin : GameResult.WhiteWin;
            // else
            return IsDraw() ? GameResult.Draw : GameResult.OnGoing;
        }
        catch
        {
            return await MakeTurn(player);
        }
    }
    
    public async Task<GameResult> Play()
    {
        while (true)
        {
            var result = await MakeTurn(blackPlayer);
            if (result != GameResult.OnGoing)
                return result;
            
            result = await MakeTurn(whitePlayer);
            if (result != GameResult.OnGoing)
                return result;
        }
    }
}
