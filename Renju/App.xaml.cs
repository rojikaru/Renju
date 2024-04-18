namespace Renju;

public partial class App
{
    public App(MenuPage page)
    {
        InitializeComponent();
        MainPage = new NavigationPage(page);
    }
}