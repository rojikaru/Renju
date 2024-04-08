namespace Renju.Views;

public partial class RenjuPage
{
    public RenjuPage(RenjuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}