namespace Renju;

public partial class App
{
    public App(MenuPage mainPage)
    {
        InitializeComponent();
        MainPage = new NavigationPage(mainPage);
    }
}