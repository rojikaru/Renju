namespace RenjuLib.Round;

public class GameRound(
    IPlayer blackPlayer,
    IPlayer whitePlayer
)
{
    private RenjuBoard RenjuBoard { get; } = new();
    
    public GameResult Result { get; private set; }

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
        bool isBlack = true;
        while (true)
        {
            var result = await MakeTurn(isBlack ? blackPlayer : whitePlayer);
            if (result != GameResult.OnGoing)
            {
                Result = result;
                return result;
            }
            
            isBlack = !isBlack;
        }
    }
}
