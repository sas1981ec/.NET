using Xamarin.Forms;
using MusicStore.Views;
using MusicStore.Models;

namespace MusicStore
{
    public partial class App : Application
    {
        static UserDB database;

        public static UserDB Database
        {
            get
            {

                if (database == null)
                {
                    database = new UserDB(DependencyService.Get<IStdLocHelper>().GetLocalFilePath("userdb.db"));
                }

                return database;
            }

        }
        public App()
        {
            InitializeComponent();

           // DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
