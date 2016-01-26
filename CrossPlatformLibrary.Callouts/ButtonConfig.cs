using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CrossPlatformLibrary.Callouts
{
    /// <summary>
    /// ButtonConfig is the platform-independent abstraction
    /// of a dialog button.
    /// </summary>
    public class ButtonConfig : INotifyPropertyChanged
    {
        private bool isEnabled;
        
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
                    this.OnPropertyChanged();
                    this.OnEnabledChanged(value);
                }
            }
        }

        public event EventHandler<bool> EnabledChanged;

        protected virtual void OnEnabledChanged(bool e)
        {
            var handler = this.EnabledChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}