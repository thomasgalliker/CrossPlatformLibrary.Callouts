using System;

namespace CrossPlatformLibrary.Callouts
{
    /// <summary>
    /// ButtonConfig is the platform-independent abstraction
    /// of a dialog button.
    /// </summary>
    public class ButtonConfig
    {
        private bool isEnabled;

        public event EventHandler<bool> EnabledChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonConfig"/> class.
        /// </summary>
        /// <param name="text">The button text to be shown.</param>
        /// <param name="action">The action that is executed when the button is pressed.</param>
        /// <param name="isEnabled">Indicates if the button is enabled or disabled.</param>
        public ButtonConfig(string text, Action action = null, bool isEnabled = true)
        {
            this.Text = text;
            if (action == null)
            {
                action = () => { };
            }
            this.Action = action;
            this.IsEnabled = isEnabled;
        }

        public string Text { get; private set; }

        public Action Action { get; private set; }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    this.OnEnabledChanged(value);
                }
            }
        }

        ////public bool IsPositive { get; set; }

        ////public bool IsNegative { get; set; }

        protected virtual void OnEnabledChanged(bool e)
        {
            this.EnabledChanged?.Invoke(this, e);
        }
    }
}