namespace Renju.ViewModels;

public sealed class RenjuViewModel : ObservableObject
{
    #region data

    private ISession _currentGameSession;
    public ISession CurrentGameSession { 
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
        set => SetProperty(ref _board, value);
    }

    private Intersection? _lastMove;

    private IMessageService MessageService { get; }

    #endregion

    #region commands

    public ICommand BoardClickCmd { get; }

    public ICommand NewGameCommand { get; }

    public ICommand SaveGameCommand { get; }

    public ICommand LoadGameCommand { get; }

    // TODO: Implement undo/redo

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
        Board = [];

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

    private Func<Task<Move>> CreateMoveAwaiter(IPlayer player)
        => async () =>
        {
            // Wait for the player to make a move
            do
            {
                await Task.Delay(300);
            } while (_lastMove is null);

            // TODO: Add player info
            Move move = new(_lastMove.X, _lastMove.Y, player.Color);
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
        // TODO: Add confirmation dialog
        // TODO: Implement save/load game

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
