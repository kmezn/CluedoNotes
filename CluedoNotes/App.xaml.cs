using CluedoNotes.Data;

namespace CluedoNotes
{
    public partial class App : Application
    {
        public static DBService _dbService {  get; private set; }
        public App(DBService db)
        {
            InitializeComponent();
            UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Dark;
            MainPage = new MainPage();
            _dbService = db;
        }
    }
}