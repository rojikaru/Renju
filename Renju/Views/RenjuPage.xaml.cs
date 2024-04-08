namespace Renju.Views;

public partial class RenjuPage
{
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
        Window.MinimumHeight = 300;
        Window.MinimumWidth = 600;
    }
}