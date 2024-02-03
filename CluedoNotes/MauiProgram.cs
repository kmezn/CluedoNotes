using CluedoNotes.Data;
using Microsoft.Extensions.Logging;

namespace CluedoNotes
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            // Set path to the SQLite database (it will be created if it does not exist)
            var dbPath =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"CluedoNotes.db");
#if DEBUG
            dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), 
                @"CluedoNotes.db");
#endif
            // Register WeatherForecastService and the SQLite database
            IServiceCollection serviceCollection = builder.Services.AddSingleton(
                s => ActivatorUtilities.CreateInstance<DBService>(s, dbPath));
            builder.Services.AddSingleton<SetupCardService>();
            builder.Services.AddSingleton<SetupPlayerService>();

            return builder.Build();
        }
    }
}