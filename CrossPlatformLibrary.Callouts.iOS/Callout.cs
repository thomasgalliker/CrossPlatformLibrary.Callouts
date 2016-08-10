using System;

using CrossPlatformLibrary.Dispatching;

using Guards;

#if __UNIFIED__
using Foundation;
using UIKit;
using CoreGraphics;
#else
using MonoTouch.Foundation;
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

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var alertController = UIAlertController.Create(caption, string.Empty, UIAlertControllerStyle.Alert);
                if (stringContent != null)
                {
                    alertController.Message = stringContent;
                }

                var view = content as UIView;
                if (view != null)
                {
                    UIViewController v = new UIViewController();
                    v.View = view;
                    alertController.SetValueForKey(v, new NSString("contentViewController"));

                    //alertController.View.AddSubview(view);
                }

                foreach (var buttonConfig in buttonConfigs)
                {
                    var alertAction = UIAlertAction.Create(buttonConfig.Text, UIAlertActionStyle.Default, x => buttonConfig.Action());

                    EventHandler<bool> buttonConfigOnEnabledChanged = null;
                    buttonConfigOnEnabledChanged = (sender, isEnabled) => { alertAction.Enabled = isEnabled; };
                    buttonConfig.EnabledChanged += buttonConfigOnEnabledChanged;
                    alertAction.Enabled = buttonConfig.IsEnabled;
                    alertController.AddAction(alertAction);
                }

                this.dispatcherService.CheckBeginInvokeOnUI(() => { UIApplication.SharedApplication.PresentInternal(alertController); });
            }
            else
            {
                var alertView = new UIAlertView
                {
                    AlertViewStyle = UIAlertViewStyle.Default,
                    Title = caption
                };

                if (stringContent != null)
                {
                    alertView.Message = stringContent;
                }

                var view = content as UIView;
                if (view != null)
                {
                    alertView.AddSubview(view);
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