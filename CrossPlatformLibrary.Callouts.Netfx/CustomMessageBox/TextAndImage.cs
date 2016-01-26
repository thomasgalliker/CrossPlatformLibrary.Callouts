using System.Windows.Media;

namespace CrossPlatformLibrary.Callouts.CustomMessageBox
{
    /// <summary>
    /// This class is used for binding text and image to the datatemplate.
    /// </summary>
    internal class TextAndImage
    {
        public TextAndImage(string messageBoxText, ImageSource messageBoxImage = null)
        {
            this.MessageBoxText = messageBoxText;
            this.MessageBoxImage = messageBoxImage;
        }

        public string MessageBoxText { get; private set; }

        public ImageSource MessageBoxImage { get; private set; }
    }
}