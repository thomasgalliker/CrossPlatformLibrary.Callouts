namespace CrossPlatformLibrary.Callouts
{
    public abstract class CalloutBase : ICallout
    {
        /// <inheritdoc />
        public void Show(string caption, object content)
        {
            var defaultButtonConfig = new[] { new ButtonConfig("OK") };

            this.Show(caption, content, defaultButtonConfig);
        }

        /// <inheritdoc />
        public abstract void Show(string caption, object content, ButtonConfig[] buttonConfigs, bool isFullScreen = false);
    }
}