namespace RenjuLib.Session;

public interface ISession
{
    IPlayer BlackPlayer { get; }
    
    IPlayer WhitePlayer { get; }
    
    Task Play();
    
    event Action? BoardChanged;
    
    IEnumerable<GameRound> Rounds { get; }
    
    GameRound CurrentRound { get; }
}
