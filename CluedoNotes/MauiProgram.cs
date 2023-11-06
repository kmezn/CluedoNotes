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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = FileAccessHelper.GetLocalFilePath("players.db3");
            builder.Services.AddSingleton<PlayerRepository>(s => ActivatorUtilities.CreateInstance<PlayerRepository>(s, dbPath));

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }

    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}