namespace Renju.ViewModels
{
    public class MenuViewModel
    {
        #region Properties

        #endregion

        #region Commands

        public ICommand HumanVsHumanCommand { get; set; }
        public ICommand HumanVsBotCommand { get; set; }
        public ICommand TournamentCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        #endregion

        #region Constructors

        public MenuViewModel()
        {
            HumanVsHumanCommand = new RelayCommand(HumanVsHumanExecute, HumanVsHumanCanExecute);
            HumanVsBotCommand = new RelayCommand(HumanVsBotExecute, HumanVsBotCanExecute);
            TournamentCommand = new RelayCommand(TournamentExecute, TournamentCanExecute);
            SettingsCommand = new RelayCommand(SettingsExecute, SettingsCanExecute);
        }

        #endregion

        #region Methods

        public void HumanVsHumanExecute()
        {
            if (Application.Current is null) return;
            Application.Current.MainPage = new RenjuPage(new RenjuViewModel(new MessageService()));
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
