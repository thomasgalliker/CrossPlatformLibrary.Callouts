using System;

using CrossPlatformLibrary.Dispatching;

using Guards;

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

            throw new NotImplementedException();
        }
    }
}