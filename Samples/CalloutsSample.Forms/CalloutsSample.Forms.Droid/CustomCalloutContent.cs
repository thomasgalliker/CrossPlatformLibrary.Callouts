using Android.Content;
using Android.Views;
using Android.Widget;

using CrossPlatformLibrary.Callouts;

namespace CalloutsSample.Forms.Droid
{
    public class CustomCallout : ICustomCallout
    {
        public object GetContent(ButtonConfig okButtonConfig)
        {
            var inflatorservice = (LayoutInflater)Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService);
            var containerView = inflatorservice.Inflate(Resource.Layout.customcallout, null, false);

            var @switch = (Switch)containerView.FindViewById(Resource.Id.switch1);
            @switch.CheckedChange += (sender, args) =>
            {
                okButtonConfig.IsEnabled = args.IsChecked;
            };

            return containerView;
        }
    }
}
