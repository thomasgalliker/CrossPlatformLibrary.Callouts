using Android.App;
using Android.Content.PM;
using Android.OS;

using CrossPlatformLibrary.IoC;

using Xamarin.Forms.Platform.Android;

namespace CalloutsSample.Forms.Droid
{
    [Activity(Label = "CalloutsSample.Forms", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SimpleIoc.Default.Register<ICustomCallout, CustomCallout>(); // TODO: Register by convention!

            Xamarin.Forms.Forms.Init(this, bundle);
            this.LoadApplication(new App());
        }
    }
}