using System;

using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;

namespace CrossPlatformLibrary.Callouts
{
    public class Callout : CalloutBase
    {
        /// <inheritdoc />
        public override void Show(string caption, object content, ButtonConfig[] buttonConfigs, bool isFullScreen = false)
        {
            //http://www.reflectionit.nl/blog/2015/windows-10-xaml-tips-messagedialog-and-contentdialog
            //http://www.kunal-chowdhury.com/2013/02/win8dev-tutorial-windows-store-winrt-messagedialog.html
            //https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.contentdialog.aspx
            //ContentDialog contentDialog = new ContentDialog(); == TODO CHeck for examples 

            var stringContent = content as string;
            if (stringContent != null)
            {
                var messageDialog = new MessageDialog(stringContent, caption);
                foreach (var buttonConfig in buttonConfigs)
                {
                    var config = buttonConfig;
                    messageDialog.Commands.Add(new UICommand(buttonConfig.Text, delegate { config.Action(); }));
                }

                messageDialog.ShowAsync();
            }
            else
            {
                ButtonConfig primaryButton = null;
                ButtonConfig secondaryButton = null;

                if (buttonConfigs.Length < 1 || buttonConfigs.Length > 2)
                {
                    throw new ArgumentException("'buttonConfigs' only supports ");
                }

                var dialog = new ContentDialog
                {
                    Title = caption,
                    //RequestedTheme = ElementTheme.Dark,
                    //FullSizeDesired = isFullScreen,
                    //MaxWidth = this.ActualWidth // Required for Mobile!
                };

                var cp = new ContentPresenter();
                cp.Content = content;

#if NETFX_CORE
                dialog.Content = cp;
#else
                  dialog.ContentWrapper = cp;
#endif

                if (buttonConfigs.Length >= 1)
                {
                    primaryButton = buttonConfigs[0];
                    dialog.PrimaryButtonText = primaryButton.Text;
                    dialog.IsPrimaryButtonEnabled = true;
                    dialog.PrimaryButtonClick += delegate { primaryButton.Action(); };
                }
                if (buttonConfigs.Length == 2)
                {
                    secondaryButton = buttonConfigs[1];
                    dialog.SecondaryButtonText = secondaryButton.Text;
                    dialog.IsSecondaryButtonEnabled = true;
                    dialog.SecondaryButtonClick += delegate { secondaryButton.Action(); };
                }

                dialog.ShowAsync();
            }
        }
    }
}