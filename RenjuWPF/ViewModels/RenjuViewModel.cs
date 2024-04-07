namespace RenjuWPF.ViewModels;

// ReSharper disable UnusedAutoPropertyAccessor.Global
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

        blackPlayer.AwaitMove += CreateMoveAwaiter(blackPlayer);
        whitePlayer.AwaitMove += CreateMoveAwaiter(whitePlayer);

        CurrentGameSession = new SingleSession(blackPlayer, whitePlayer);
        CurrentGameSession.BoardChanged += OnBoardChanged;
        OnBoardChanged();

        CurrentGameSession.GameEnded += () =>
        {
            MessageBox.Show(
                CurrentGameSession.Result switch
                {
                    GameResult.BlackWon => "Black player wins!",
                    GameResult.WhiteWon => "White player wins!",
                    GameResult.Draw => "It's a draw!",
                    _ => "Unknown result"
                },
                "Game over!",
                MessageBoxButton.OK
            );
        };

        CurrentGameSession.Play();
    }

    #endregion

    #region methods

    private void OnBoardClick(Intersection? i)
    {
        Console.WriteLine(i);
        
        if (i is null)
            return;

        if (i.Stone != CellStone.Empty)
        {
            // TODO: Add some kind of error message
            return;
        }

        _lastMove = i;
    }

    private Func<Task<Move>> CreateMoveAwaiter(IPlayer player)
        => async () =>
        {
            // Wait for the player to make a move
            do
            {
                await Task.Delay(400);
            } while (_lastMove is null);

            // TODO: Add player info
            var move = new Move(_lastMove.X, _lastMove.Y, player.Color);
            _lastMove = null;

            return move;
        };

    private void OnBoardChanged()
        => Board = new ObservableCollection<Intersection>(
            CurrentGameSession.CurrentBoard.Intersections
        );

    #endregion
}
