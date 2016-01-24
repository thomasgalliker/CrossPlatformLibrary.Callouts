using System;
using System.Windows;

using CrossPlatformLibrary.Dispatching;

using Guards;

using WPFCustomMessageBox;

namespace CrossPlatformLibrary.Callouts
{
    /// <summary>
    /// Callout implementation for .Net/WPF.
    /// </summary>
    public class Callout : CalloutBase
    {
        /// <inheritdoc />
        public override void Show(string caption, object content, ButtonConfig[] buttonConfigs, bool isFullScreen = false)
        {
            Guard.ArgumentNotNull(() => buttonConfigs);

            var result = CustomMessageBox.Show(
                messageBoxText: (string)content, 
                caption: caption, button: MessageBoxButton.OK);
        }
    }
}