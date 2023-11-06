namespace CluedoNotes
{
    public partial class App : Application
    {
        public static PlayerRepository PlayerRepo {  get; private set; }
        public App(PlayerRepository repo)
        {
            InitializeComponent();

            MainPage = new AppShell();
            PlayerRepo = repo;
        }
    }
}