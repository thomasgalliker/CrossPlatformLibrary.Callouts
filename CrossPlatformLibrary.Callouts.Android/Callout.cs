using System;

using Android.App;

using CrossPlatformLibrary.Dispatching;

using Guards;

namespace CrossPlatformLibrary.Callouts
{
    /// <summary>
    /// Callout implementation for Android.
    /// </summary>
    public class Callout : CalloutBase
    {
        private readonly IDispatcherService dispatcherService;

        public Callout(IDispatcherService dispatcherService)
        {
            Guard.ArgumentNotNull(() => dispatcherService);

            this.dispatcherService = dispatcherService;
        }

        /// <inheritdoc />
        public override void Show(string caption, object content, ButtonConfig[] buttonConfigs, bool isFullScreen = false)
        {
            Guard.ArgumentNotNull(() => buttonConfigs);

            var context = Application.Context;

            var alert = new AlertDialog.Builder(context);
            alert.SetTitle(caption);

            var stringContent = content as string;
            if (stringContent != null)
            {
                alert.SetMessage(stringContent);
            }

            foreach (var buttonConfig in buttonConfigs)
            {
                var config = buttonConfig;
                alert.SetNeutralButton(buttonConfig.Text, (senderAlert, args) => { config.Action(); });
            }

            // dispatch the alert to the UI thread
            this.dispatcherService.CheckBeginInvokeOnUI(() => alert.Show());
        }
    }
}