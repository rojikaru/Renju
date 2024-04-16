namespace Renju.Views;

public partial class RenjuPage : ContentPage
{
    private const double MinBoardSize = 850;
    
    public RenjuPage(RenjuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    
    protected override void OnAppearing()
    {
        if (DeviceInfo.Current.Idiom != DeviceIdiom.Desktop ||
            Window is null) 
            return;
        
        base.OnAppearing();
        Window.MinimumWidth = Window.MinimumHeight = MinBoardSize;
    }
}