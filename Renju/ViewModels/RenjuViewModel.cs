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

    public ICommand NewGameCommand { get; set; }

    public ICommand SaveGameCommand { get; set; }
    
    public ICommand LoadGameCommand { get; set; }
    
    public ICommand UndoMoveCommand { get; set; } // TODO: Implement undo/redo
    
    public ICommand RedoMoveCommand { get; set; }

    public ICommand ExitCommand { get; set; }
    
    public ICommand AboutCommand { get; set; }
    
    public ICommand RulesCommand { get; set; }
    
    public ICommand SettingsCommand { get; set; }

    #endregion

    #region constructors

    public RenjuViewModel(IMessageService messageService)
    {
        MessageService = messageService;
        
        BoardClickCmd = new RelayCommand<Intersection>(OnBoardClick);
        NewGameCommand = new AsyncRelayCommand(NewGameExecute, NewGameCanExecute);
        SaveGameCommand = new AsyncRelayCommand(SaveGameExecute, SaveGameCanExecute);
        LoadGameCommand = new AsyncRelayCommand(LoadGameExecute, LoadGameCanExecute);
        UndoMoveCommand = new AsyncRelayCommand(UndoMoveExecute, UndoMoveCanExecute);
        RedoMoveCommand = new AsyncRelayCommand(RedoMoveExecute, RedoMoveCanExecute);
        ExitCommand = new AsyncRelayCommand(ExitExecute, ExitCanExecute);
        AboutCommand = new AsyncRelayCommand(AboutExecute, AboutCanExecute);
        RulesCommand = new AsyncRelayCommand(RulesExecute, RulesCanExecute);
        SettingsCommand = new AsyncRelayCommand(SettingsExecute, SettingsCanExecute);

        // TODO: Implement save/load game, players/session injection
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

    private async Task NewGameExecute()
    {
        throw new NotImplementedException();
    }
    
    private bool NewGameCanExecute() => false;
    
    private async Task SaveGameExecute()
    {
        throw new NotImplementedException();
    }
    
    private bool SaveGameCanExecute() => false;
    
    private async Task LoadGameExecute()
    {
        throw new NotImplementedException();
    }
    
    private bool LoadGameCanExecute() => false;
    
    private async Task UndoMoveExecute()
    {
        throw new NotImplementedException();
    }
    
    private bool UndoMoveCanExecute() => false;
    
    private async Task RedoMoveExecute()
    {
        throw new NotImplementedException();
    }
    
    private bool RedoMoveCanExecute() => false;
    
    private async Task ExitExecute()
    {
        // TODO: Add confirmation dialog and save game

        Application.Current?.Quit();
        // For iOS
        Environment.Exit(0);
    }
    
    private bool ExitCanExecute() => true;
    
    private async Task AboutExecute()
    {
        throw new NotImplementedException();
    }
    
    private bool AboutCanExecute() => false;
    
    private async Task RulesExecute()
    {
        throw new NotImplementedException();
    }
    
    private bool RulesCanExecute() => false;
    
    private async Task SettingsExecute()
    {
        throw new NotImplementedException();
    }
    
    private bool SettingsCanExecute() => false;

    #endregion
}