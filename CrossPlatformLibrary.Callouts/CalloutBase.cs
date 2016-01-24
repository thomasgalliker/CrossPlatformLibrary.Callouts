namespace CrossPlatformLibrary.Callouts
{
    public abstract class CalloutBase : ICallout
    {
        protected CalloutBase()
        {
            this.MinNumberOfButtons = 1;
            this.MaxNumberOfButtons = int.MaxValue;
        }

        public int MinNumberOfButtons { get; private set; }

        public int MaxNumberOfButtons { get; protected set; }

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