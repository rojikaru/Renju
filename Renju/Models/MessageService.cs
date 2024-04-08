namespace Renju.Models;

public class MessageService : IMessageService
{
    public async Task ShowAsync(string title, string message, string cancel)
    {
        await Application.Current?.MainPage?.Dispatcher.DispatchAsync(() =>
        {
            Application.Current.MainPage.DisplayAlert(title, message, cancel);
        })!;
    }
}