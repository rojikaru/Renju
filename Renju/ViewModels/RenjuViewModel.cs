namespace Renju.ViewModels;

public sealed class RenjuViewModel : ObservableObject
{
    #region data

    private ISession _currentGameSession;

    public ISession CurrentGameSession
    {
        get => _currentGameSession;
        set
        {
            _currentGameSession = value;

            if (value?.BlackPlayer is HumanPlayer humanBlack)
                humanBlack.AwaitMove += CreateMoveAwaiter(humanBlack);
            if (value?.WhitePlayer is HumanPlayer humanWhite)
                humanWhite.AwaitMove += CreateMoveAwaiter(humanWhite);

            if (value is not null)
                Board = value.CurrentBoard.Intersections;
        }
    }

    private ObservableCollection<Intersection> _board;

    public ObservableCollection<Intersection> Board
    {
        get => _board;
        set => SetProperty(ref _board, value, nameof(Board));
    }

    private Intersection? _lastMove;

    private IMessageService MessageService { get; }

    private IPlayer BlackPlayer => CurrentGameSession.BlackPlayer;

    private IPlayer WhitePlayer => CurrentGameSession.WhitePlayer;

    #endregion

    #region commands

    public ICommand BoardClickCmd { get; }

    public ICommand NewGameCommand { get; }

    public ICommand SaveGameCommand { get; }

    public ICommand LoadGameCommand { get; }

    public ICommand UndoMoveCommand { get; }

    public ICommand RedoMoveCommand { get; }

    public ICommand ExitCommand { get; }

    public ICommand AboutCommand { get; }

    public ICommand RulesCommand { get; }

    public ICommand SettingsCommand { get; }

    #endregion

    #region constructors

    public RenjuViewModel(IMessageService messageService)
    {
        MessageService = messageService;
        _currentGameSession = null!;
        _board = [];

        BoardClickCmd = new RelayCommand<Intersection>(OnBoardClick);
        NewGameCommand = new AsyncRelayCommand(
            NewGameExecute,
            NewGameCanExecute
        );
        SaveGameCommand = new AsyncRelayCommand(
            SaveGameExecute,
            SaveGameCanExecute
        );
        LoadGameCommand = new AsyncRelayCommand(
            LoadGameExecute,
            LoadGameCanExecute
        );
        UndoMoveCommand = new AsyncRelayCommand(
            UndoMoveExecute,
            UndoMoveCanExecute
        );
        RedoMoveCommand = new AsyncRelayCommand(
            RedoMoveExecute,
            RedoMoveCanExecute
        );
        ExitCommand = new AsyncRelayCommand(
            ExitExecute,
            ExitCanExecute
        );
        AboutCommand = new AsyncRelayCommand(
            AboutExecute,
            AboutCanExecute
        );
        RulesCommand = new AsyncRelayCommand(
            RulesExecute,
            RulesCanExecute
        );
        SettingsCommand = new AsyncRelayCommand(
            SettingsExecute,
            SettingsCanExecute
        );
    }

    public RenjuViewModel(ISession session, IMessageService messageService)
        : this(messageService)
    {
        CurrentGameSession = session;
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

    private Func<CancellationToken, Task<Move>> CreateMoveAwaiter(IPlayer player)
        => async (token) =>
        {
            // Wait for the player to make a move
            try
            {
                do
                {
                    await Task.Delay(300, token);
                } while (_lastMove is null);
            }
            catch (TaskCanceledException)
            {
                return new Move(-1, -1, player.Color);
            }

            // TODO: Add player info
            Move move = new(_lastMove.X, _lastMove.Y, player.Color);
            _lastMove = null;

            return move;
        };

    private async Task NewGameExecute()
    {
        CurrentGameSession.Terminate();
        CurrentGameSession = CurrentGameSession.Clone();
        CurrentGameSession.GameEnded += Session.AlertOnGameEnded(
            CurrentGameSession,
            MessageService
        );
        _ = CurrentGameSession.Play();
    }

    private bool NewGameCanExecute() => true;

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
        // TODO: Implement undo
        throw new NotImplementedException();
    }

    private bool UndoMoveCanExecute() => false;

    private async Task RedoMoveExecute()
    {
        // TODO: Implement redo
        throw new NotImplementedException();
    }

    private bool RedoMoveCanExecute() => false;

    private async Task ExitExecute()
    {
        // TODO: Implement saving game on exit

        bool result = await MessageService.ShowAsync(
            "Exit game?",
            "Do you want to exit the game?",
            "Yes",
            "No"
        );

        if (!result) return;


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
