using CluedoNotes.Repos;

namespace CluedoNotes
{
    public partial class App : Application
    {
        public static CluedoContext context {  get; private set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            context = new CluedoContext();
        }
    }
}