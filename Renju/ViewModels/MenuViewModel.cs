namespace Renju.ViewModels
{
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
            HumanVsBotCommand = new RelayCommand(
                HumanVsBotExecute,
                HumanVsBotCanExecute
            );
            TournamentCommand = new RelayCommand(
                TournamentExecute,
                TournamentCanExecute
            );
            SettingsCommand = new RelayCommand(
                SettingsExecute,
                SettingsCanExecute
            );
            MessageService = messageService;
        }

        #endregion

        #region Methods

        private async Task HumanVsHumanExecute()
        {
            ISession session = new SingleSession(
                new HumanPlayer(CellStone.Black, "Player 1"),
                new HumanPlayer(CellStone.White, "Player 2")
            );
            session.GameEnded += OnGameEnded(session);

            RenjuViewModel vm = (RenjuViewModel)RenjuPage.BindingContext;
            vm.CurrentGameSession = session;
            await Navigation.PushAsync(RenjuPage);
            _ = session.Play();
        }

        private Action OnGameEnded(ISession session)
        {
            return () => MessageService.ShowAsync
            (
                "Game over!",
                session.Result switch
                {
                    GameResult.BlackWon => "Black player wins!",
                    GameResult.WhiteWon => "White player wins!",
                    GameResult.Draw => "It's a draw!",
                    _ => "Unknown result"
                },
                "OK"
            );
        }

        private bool HumanVsHumanCanExecute() => true;

        private void HumanVsBotExecute()
        {
            throw new NotImplementedException();
        }

        private bool HumanVsBotCanExecute() => false;

        private void TournamentExecute()
        {
            throw new NotImplementedException();
        }

        private bool TournamentCanExecute() => false;

        private void SettingsExecute()
        {
            throw new NotImplementedException();
        }

        public bool SettingsCanExecute() => false;

        #endregion
    }
}
