namespace Renju.ViewModels;

public class MenuViewModel
{
    #region Properties

    private INavigation Navigation { get; }

    private RenjuPage RenjuPage { get; }

    private IMessageService MessageService { get; }

    #endregion

    #region Commands

    public ICommand HumanVsHumanCommand { get; }

    public ICommand HumanVsBotCommand { get; }

    public ICommand TournamentCommand { get; }

    public ICommand SettingsCommand { get; }

    #endregion

    #region Constructors

    public MenuViewModel(
        INavigation navigation,
        RenjuPage renjuPage,
        IMessageService messageService
    )
    {
        Navigation = navigation;
        RenjuPage = renjuPage;

        HumanVsHumanCommand = new AsyncRelayCommand(
            HumanVsHumanExecute,
            HumanVsHumanCanExecute
        );
        HumanVsBotCommand = new AsyncRelayCommand(
            HumanVsBotExecute,
            HumanVsBotCanExecute
        );
        TournamentCommand = new AsyncRelayCommand(
            TournamentExecute,
            TournamentCanExecute
        );
        SettingsCommand = new AsyncRelayCommand(
            SettingsExecute,
            SettingsCanExecute
        );
        MessageService = messageService;
    }

    #endregion

    #region Methods

    private async Task HumanVsHumanExecute()
    {
        SingleSession session = new(
            new HumanPlayer(CellStone.Black, "Player 1"),
            new HumanPlayer(CellStone.White, "Player 2")
        );
        session.GameEnded += Session.AlertOnGameEnded(
            session,
            MessageService
        );

        RenjuViewModel vm = (RenjuViewModel)RenjuPage.BindingContext;
        vm.CurrentGameSession = session;
        await Navigation.PushAsync(RenjuPage);
        _ = session.Play();
    }

    private bool HumanVsHumanCanExecute() => true;

    private async Task HumanVsBotExecute()
    {
        throw new NotImplementedException();
    }

    private bool HumanVsBotCanExecute() => false;

    private async Task TournamentExecute()
    {
        throw new NotImplementedException();
    }

    private bool TournamentCanExecute() => false;

    private async Task SettingsExecute()
    {
        throw new NotImplementedException();
    }

    private bool SettingsCanExecute() => false;

    #endregion
}