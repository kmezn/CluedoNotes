using CluedoNotes.Data;
using Microsoft.Extensions.Logging;

namespace CluedoNotes
{
    public static class MauiProgram
    {
        public const string DatabaseFilename = @"CluedoNotes.db";
        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
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
        // Register the SQLite database services and viewmodel services
        IServiceCollection serviceCollection = builder.Services.AddSingleton(
                s => ActivatorUtilities.CreateInstance<DBService>(s));
            builder.Services.AddSingleton<CardService>();
            builder.Services.AddSingleton<PlayerService>();

            return builder.Build();
        }
    }
}