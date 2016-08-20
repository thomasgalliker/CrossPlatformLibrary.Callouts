namespace CrossPlatformLibrary.Callouts
{
    public interface ICallout
    {
        /// <summary>
        /// Shows a message box with the specified <param name="caption">caption</param> as title
        /// and <param name="content">content</param> in the body of the dialogue.
        /// The presented message box will have one button with text 'OK' and no button action.
        /// </summary>
        /// <param name="caption">The caption (title) of the message box.</param>
        /// <param name="content">The body of the message box. This can be either a simple string or a complex object (e.g. a view with viewmodel). </param>
        void Show(string caption, object content);

        /// <summary>
        /// Shows a message box with the specified <param name="caption">caption</param> as title
        /// and <param name="content">content</param> in the body of the dialogue.
        /// </summary>
        /// <param name="caption">The caption (title) of the message box.</param>
        /// <param name="content">The body of the message box. This can be either a simple string or a complex object (e.g. a view with viewmodel). </param>
        /// <param name="buttonConfigs">The buttons to be shown at the bottom of the message box.</param>
        /// <param name="isFullScreen">Shows the message box in fullscreen mode if <value>true</value>. Default is <value>false</value>.</param>
        void Show(string caption, object content, ButtonConfig[] buttonConfigs, bool isFullScreen = false);
    }
}