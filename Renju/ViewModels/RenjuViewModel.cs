using CommunityToolkit.Mvvm.ComponentModel;

namespace Renju.ViewModels;

public sealed class RenjuViewModel : ObservableObject
{
    #region data

    private ISession CurrentGameSession { get; }

    private ObservableCollection<Intersection>? _board;
    public ObservableCollection<Intersection> Board
    {
        get => _board!;
        private set => SetProperty(ref _board, value);
    }
    
    private Intersection? _lastMove;

    #endregion

    #region commands

    public ICommand BoardClickCmd { get; }
    
    // add more commands here

    #endregion

    #region constructors
    
    public RenjuViewModel()
    {
        BoardClickCmd = new RelayCommand<Intersection>(OnBoardClick);
        
        // Grab the current game session from the database
        var blackPlayer = new HumanPlayer(CellStone.Black, "A");
        var whitePlayer = new HumanPlayer(CellStone.White, "B");
        
        blackPlayer.AwaitMove += MoveAwaiter(blackPlayer);
        whitePlayer.AwaitMove += MoveAwaiter(whitePlayer);
        
        CurrentGameSession = new SingleSession(blackPlayer, whitePlayer);
        CurrentGameSession.BoardChanged += OnBoardChanged;
        OnBoardChanged();
        
        CurrentGameSession.Play();
    }
    
    #endregion

    #region methods

    private void OnBoardClick(Intersection? i)
    {
        if (i is null || i.Stone != CellStone.None)
            return;
        
        _lastMove = i;
    }

    private Func<Task<Move>> MoveAwaiter(IPlayer player)
        => async () =>
        {
            // Wait for the player to make a move
            do
            {
                await Task.Delay(100);
            } while (_lastMove is null);

            // TODO: Add player info
            var move = new Move(_lastMove.X, _lastMove.Y, player.Color);
            _lastMove = null;
            
            return move;
        };

    private void OnBoardChanged() 
        => Board = new(CurrentGameSession.CurrentRound.RenjuBoard.Intersections);
    
    #endregion
}
