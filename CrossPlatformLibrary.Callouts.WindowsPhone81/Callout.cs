using System;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

using Guards;

namespace CrossPlatformLibrary.Callouts
{
    public class Callout : CalloutBase
    {
        public Callout()
        {
            this.MaxNumberOfButtons = 2;
        }

        /// <inheritdoc />
        public override void Show(string caption, object content, ButtonConfig[] buttonConfigs, bool isFullScreen = false)
        {
            Guard.ArgumentNotNull(() => buttonConfigs);

            ButtonConfig primaryButton = null;
            ButtonConfig secondaryButton = null;

            if (buttonConfigs.Length < this.MinNumberOfButtons || buttonConfigs.Length > this.MaxNumberOfButtons)
            {
                throw new ArgumentException(string.Format("'buttonConfigs' supports a minimum of {0} and a maximum of {1} buttons", this.MinNumberOfButtons, this.MaxNumberOfButtons));
            }

            if (buttonConfigs.Length >= 1)
            {
                primaryButton = buttonConfigs[0];
            }

            if (buttonConfigs.Length == 2)
            {
                secondaryButton = buttonConfigs[1];
            }

            //http://www.reflectionit.nl/blog/2015/windows-10-xaml-tips-messagedialog-and-contentdialog
            //http://www.kunal-chowdhury.com/2013/02/win8dev-tutorial-windows-store-winrt-messagedialog.html
            //https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.contentdialog.aspx
            //ContentDialog contentDialog = new ContentDialog(); == TODO CHeck for examples 

            var stringContent = content as string;
            if (stringContent != null)
            {
                var messageDialog = new MessageDialog(stringContent, caption);

                if (primaryButton != null)
                {
                    messageDialog.Commands.Add(new UICommand(primaryButton.Text, delegate { primaryButton.Action(); }));
                }
                if (secondaryButton != null)
                {
                    messageDialog.Commands.Add(new UICommand(secondaryButton.Text, delegate { secondaryButton.Action(); }));
                }

                messageDialog.ShowAsync();
            }
            else
            {
                var contentDialog = new ContentDialog
                {
                    Title = caption,
                    //RequestedTheme = ElementTheme.Dark,
#if WINDOWS_UWP
                    FullSizeDesired = isFullScreen,
#endif
                    //MaxWidth = this.ActualWidth // Required for Mobile!
                };

                var cp = new ContentPresenter();
                cp.Content = content;

#if WINDOWS_APP
                contentDialog.ContentWrapper = cp;
#else
                contentDialog.Content = cp;
#endif
                EventHandler<bool> primaryButtonOnEnabledChanged = null;
                if (primaryButton != null)
                {
                    primaryButtonOnEnabledChanged = (sender, isEnabled) => { contentDialog.IsPrimaryButtonEnabled = isEnabled; };
                    primaryButton.EnabledChanged += primaryButtonOnEnabledChanged;
                    contentDialog.PrimaryButtonText = primaryButton.Text;
                    contentDialog.IsPrimaryButtonEnabled = primaryButton.IsEnabled;
                    contentDialog.PrimaryButtonClick += delegate { primaryButton.Action(); };
                }

                EventHandler<bool> secondaryButtonOnEnabledChanged = null;
                if (secondaryButton != null)
                {
                    secondaryButtonOnEnabledChanged = (sender, isEnabled) => { contentDialog.IsSecondaryButtonEnabled = isEnabled; };
                    secondaryButton.EnabledChanged += secondaryButtonOnEnabledChanged;
                    contentDialog.SecondaryButtonText = secondaryButton.Text;
                    contentDialog.IsSecondaryButtonEnabled = secondaryButton.IsEnabled;
                    contentDialog.SecondaryButtonClick += delegate { secondaryButton.Action(); };
                }

#if WINDOWS_APP
                // Windows Store Apps ContentDialog does not return an async task
                // when calling ShowAsync(). TaskCompletionSource comes to the rescue!
                var tcs = new TaskCompletionSource<object>();

                contentDialog.Closed += (sender, args) => { tcs.TrySetResult(null); };
                contentDialog.ShowAsync();

                var unsubscribeTask = tcs.Task;
#else
                var unsubscribeTask = contentDialog.ShowAsync().AsTask();
#endif
                unsubscribeTask.ContinueWith(
                    ct =>
                    {
                        if (primaryButton != null && primaryButtonOnEnabledChanged != null)
                        {
                            primaryButton.EnabledChanged -= primaryButtonOnEnabledChanged;
                        }

                        if (secondaryButton != null && secondaryButtonOnEnabledChanged != null)
                        {
                            secondaryButton.EnabledChanged -= secondaryButtonOnEnabledChanged;
                        }
                    });
            }
        }
    }
}