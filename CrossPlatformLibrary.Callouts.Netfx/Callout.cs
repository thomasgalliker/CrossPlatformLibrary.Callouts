
using CrossPlatformLibrary.Callouts.CustomMessageBox;

using Guards;

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

            var messageBox = new CustomMessageBoxWindow(caption, content, buttonConfigs, isFullScreen);
            messageBox.ShowDialog();
        }
    }
}