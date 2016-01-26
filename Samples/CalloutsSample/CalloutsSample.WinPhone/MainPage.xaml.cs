using Microsoft.Phone.Controls;

using Xamarin.Forms.Platform.WinPhone;

namespace CalloutsSample.Forms.WindowsPhone8
{
    public partial class MainPage : FormsApplicationPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            Xamarin.Forms.Forms.Init();
            this.LoadApplication(new Forms.App());
        }
    }
}