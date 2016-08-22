using CrossPlatformLibrary.IoC;

namespace CalloutsSample.Forms.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            SimpleIoc.Default.Register<ICustomCallout, CustomCallout>(); // TODO: Register by convention!

            this.LoadApplication(new Forms.App());
        }
    }
}