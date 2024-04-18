namespace Renju.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage(RenjuPage renjuPage)
	{
		InitializeComponent();
		BindingContext = new MenuViewModel(Navigation, renjuPage);
	}
}