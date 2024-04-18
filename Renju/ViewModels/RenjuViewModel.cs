namespace Renju.ViewModels;

public sealed class RenjuViewModel : ObservableObject
{
    #region data

    private IMessageService MessageService { get; }

    private ISession CurrentGameSession { get; }

    public ObservableCollection<Intersection> Board { get; }

    private Intersection? _lastMove;

    #endregion

    #region commands

    public ICommand BoardClickCmd { get; }

    // add more commands here

    #endregion

    #region constructors

    public RenjuViewModel(IMessageService messageService)
    {
        BoardClickCmd = new RelayCommand<Intersection>(OnBoardClick);
        MessageService = messageService;

        // Grab the current game session from the database
        var blackPlayer = new HumanPlayer(CellStone.Black, "A");
        var whitePlayer = new HumanPlayer(CellStone.White, "B");

        blackPlayer.AwaitMove += CreateMoveAwaiter(blackPlayer);
        whitePlayer.AwaitMove += CreateMoveAwaiter(whitePlayer);

        CurrentGameSession = new SingleSession(blackPlayer, whitePlayer);
        Board = CurrentGameSession.CurrentBoard.Intersections;

        CurrentGameSession.GameEnded += async () =>
        {
            await MessageService.ShowAsync(
                "Game over!",
                CurrentGameSession.Result switch
                {
                    GameResult.BlackWon => "Black player wins!",
                    GameResult.WhiteWon => "White player wins!",
                    GameResult.Draw => "It's a draw!",
                    _ => "Unknown result"
                },
                "OK"
            );
        };

        CurrentGameSession.Play();
    }

    #endregion

    #region methods

    private void OnBoardClick(Intersection? i)
    {
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
                await Task.Delay(100);
            } while (_lastMove is null);

            // TODO: Add player info
            var move = new Move(_lastMove.X, _lastMove.Y, player.Color);
            _lastMove = null;

            return move;
        };

    #endregion
}