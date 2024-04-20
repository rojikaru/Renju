namespace Renju.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage(RenjuPage renjuPage, IMessageService msgService)
	{
		InitializeComponent();
		BindingContext = new MenuViewModel(Navigation, renjuPage, msgService);
	}
}