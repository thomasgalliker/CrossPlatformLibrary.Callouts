using System;

using CrossPlatformLibrary.Dispatching;

using Guards;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace CrossPlatformLibrary.Callouts
{
    /// <summary>
    /// Callout implementation for iOS.
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

            var stringContent = content as string;
            if (stringContent == null)
            {
                throw new NotImplementedException("Content dialogs are not yet supported.");
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var alertController = UIAlertController.Create(caption, string.Empty, UIAlertControllerStyle.Alert);
                if (stringContent != null)
                {
                    alertController.Message = stringContent;
                }
                else
                {
                    // TODO Implement content dialogs
                }

                foreach (var buttonConfig in buttonConfigs)
                {
                    alertController.AddAction(UIAlertAction.Create(buttonConfig.Text, UIAlertActionStyle.Default, x => buttonConfig.Action()));
                }

                this.dispatcherService.CheckBeginInvokeOnUI(() => { UIApplication.SharedApplication.PresentInternal(alertController); });
            }
            else
            {
                var alertView = new UIAlertView
                {
                    AlertViewStyle = UIAlertViewStyle.LoginAndPasswordInput,
                    Title = caption
                };

                if (stringContent != null)
                {
                    alertView.Message = stringContent;
                }
                else
                {
                    // TODO Implement content dialogs
                }

                foreach (var buttonConfig in buttonConfigs)
                {
                    alertView.AddButton(buttonConfig.Text);
                }

                alertView.Clicked += (s, e) => { buttonConfigs[e.ButtonIndex].Action(); };

                this.dispatcherService.CheckBeginInvokeOnUI(() => { alertView.Show(); });
            }
        }
    }
}