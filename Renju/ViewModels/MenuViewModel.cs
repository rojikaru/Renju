namespace Renju.ViewModels
{
    public class MenuViewModel
    {
        #region Properties

        public INavigation Navigation { get; }

        public RenjuPage RenjuPage { get; }

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

        public async Task HumanVsHumanExecute()
        {
            await Navigation.PushAsync(RenjuPage);
        }

        public bool HumanVsHumanCanExecute() => true;

        public void HumanVsBotExecute()
        {
            //if (Application.Current is null) return;
            //Application.Current.MainPage = new RenjuPage(new RenjuViewModel(new MessageService()));
        }

        public bool HumanVsBotCanExecute() => false;

        public void TournamentExecute()
        {
            //if (Application.Current is null) return;
            //Application.Current.MainPage = new RenjuPage(new RenjuViewModel(new MessageService()));
        }

        public bool TournamentCanExecute() => false;

        public void SettingsExecute()
        {
            //if (Application.Current is null) return;
            //Application.Current.MainPage = new RenjuPage(new RenjuViewModel(new MessageService()));
        }

        public bool SettingsCanExecute() => false;

        #endregion
    }
}
