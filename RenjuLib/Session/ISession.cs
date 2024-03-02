namespace RenjuLib.Session;

public interface ISession
{
    IPlayer BlackPlayer { get; }
    IPlayer WhitePlayer { get; }
    
    RenjuBoard Board { get; }
    
    Task Play();
    
    GameRound[] Rounds { get; }
    GameRound CurrentRound { get; }
}
