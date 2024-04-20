namespace Renju.Models;

public class MessageService : IMessageService
{
    public async Task ShowAsync(string title, string message, string cancel)
    {
        await Application.Current?.MainPage?.Dispatcher.DispatchAsync(async () =>
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        })!;
    }

    public async Task<bool> ShowAsync(string title, string message, string accept, string cancel)
    {
        bool result = false;
        await Application.Current?.MainPage?.Dispatcher.DispatchAsync(async () =>
        {
            result = await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        })!;
        return result;
    }
}