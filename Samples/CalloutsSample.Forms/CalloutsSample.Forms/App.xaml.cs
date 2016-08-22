using CrossPlatformLibrary.Bootstrapping;

using Xamarin.Forms;

namespace CalloutsSample.Forms
{
    public partial class App : Application
    {
        private readonly Bootstrapper bootstrapper;

        public App()
        {
            this.InitializeComponent();

            this.bootstrapper = new Bootstrapper();

            this.MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            this.bootstrapper.Startup();
        }

        protected override void OnSleep()
        {
            this.bootstrapper.Sleep();
        }

        protected override void OnResume()
        {
            this.bootstrapper.Resume();
        }
    }
}