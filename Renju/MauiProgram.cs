namespace Renju;

public static class MauiProgram
{
    private static IServiceCollection InjectServices(this IServiceCollection collection)
    {
        collection.AddTransient<IMessageService, MessageService>();
        return collection;
    }
    
    private static IServiceCollection InjectViews(this IServiceCollection collection)
    {
        collection.AddTransient<RenjuPage>();
        collection.AddTransient<MenuPage>();
        return collection;
    }

    // ReSharper disable once UnusedMethodReturnValue.Local
    private static IServiceCollection InjectViewModels(this IServiceCollection collection)
    {
        collection.AddTransient<RenjuViewModel>();
        return collection;
    }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services
            .InjectServices()
            .InjectViews()
            .InjectViewModels();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}