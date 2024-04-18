namespace Renju.ViewModels
{
    public class MenuViewModel
    {
        #region Properties

        private INavigation Navigation { get; }

        private RenjuPage RenjuPage { get; }

        #endregion

        #region Commands

        public ICommand HumanVsHumanCommand { get; }

        public ICommand HumanVsBotCommand { get; }

        public ICommand TournamentCommand { get; }

        public ICommand SettingsCommand { get; }

        #endregion

        #region Constructors

        public MenuViewModel(INavigation navigation, RenjuPage renjuPage)
        {
            Navigation = navigation;
            RenjuPage = renjuPage;

            HumanVsHumanCommand = new AsyncRelayCommand(HumanVsHumanExecute, HumanVsHumanCanExecute);
            HumanVsBotCommand = new RelayCommand(HumanVsBotExecute, HumanVsBotCanExecute);
            TournamentCommand = new RelayCommand(TournamentExecute, TournamentCanExecute);
            SettingsCommand = new RelayCommand(SettingsExecute, SettingsCanExecute);
        }

        #endregion

        #region Methods

        private async Task HumanVsHumanExecute()
            => await Navigation.PushAsync(RenjuPage);

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
