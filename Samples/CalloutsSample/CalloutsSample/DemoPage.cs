using System;

using CrossPlatformLibrary.Callouts;
using CrossPlatformLibrary.IoC;

using Xamarin.Forms;

namespace CalloutsSample
{
    class DemoPage : ContentPage
    {
        public DemoPage()
        {
            this.Title = "CrossPlatformLibrary.Callouts Demo";

            var button = new Button
            {
                Text = "Show simple callout"
            };
            button.Clicked += (sender, args) =>
            {
                var callout = SimpleIoc.Default.GetInstance<ICallout>();

                var buttonConfigs = new[] { new ButtonConfig("I Agree", () => { }), new ButtonConfig("I Decline") };

                callout.Show("Simple Callout", "Message with some text.", buttonConfigs);
            };
            
            var stackPanel = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { button }
            };

            this.Content = stackPanel;
        }
    }
}
