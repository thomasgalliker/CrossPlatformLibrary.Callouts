using System.Windows;

using CrossPlatformLibrary.Bootstrapping;

namespace CalloutSample.WPF
{
    public partial class App : Application
    {
        private readonly Bootstrapper bootstrapper;

        public App()
        {
            this.bootstrapper = new Bootstrapper();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.bootstrapper.Startup();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.bootstrapper.Shutdown();
            base.OnExit(e);
        }
    }
}