namespace Renju.Models;

public interface IMessageService
{
    Task ShowAsync(string title, string message, string cancel);
    Task<bool> ShowAsync(string title, string message, string accept, string cancel);
}