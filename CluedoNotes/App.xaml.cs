using CluedoNotes.Repos;

namespace CluedoNotes
{
    public partial class App : Application
    {
        public static DBRepository DBRepo {  get; private set; }
        public App(DBRepository repo)
        {
            InitializeComponent();

            MainPage = new AppShell();
            DBRepo = repo;
        }
    }
}