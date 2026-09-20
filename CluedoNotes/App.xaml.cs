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
            _dbService = db;
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            return new Window(new MainPage());
        }
    }
}