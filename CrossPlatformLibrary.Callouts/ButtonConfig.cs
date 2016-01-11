using System;

namespace CrossPlatformLibrary.Callouts
{
    /// <summary>
    /// Configures the button of a callout.
    /// </summary>
    public class ButtonConfig
    {
        public ButtonConfig(string text, Action action = null)
        {
            this.Text = text;
            if (action == null)
            {
                action = () => { };
            }
            this.Action = action;
        }

        public string Text { get; private set; }

        public Action Action { get; private set; }
    }
}